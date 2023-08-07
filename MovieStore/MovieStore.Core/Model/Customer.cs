using FluentResults;
using MovieStore.Core.Enum;
using MovieStore.Core.ValueObjects;

namespace MovieStore.Core.Model;

public class Customer
{
    public Guid Id { get; private set; }
    public Email Email { get; private set; } = null!;
    public CustomerStatus CustomerStatus { get; private set; } = null!;
    public Role Role { get; private set; }
    public Money MoneySpent { get; private set; } = null!;
    private List<PurchasedMovie> _purchasedMovies { get; } = new List<PurchasedMovie>();
    public IReadOnlyList<PurchasedMovie> PurchasedMovies => _purchasedMovies;

    public Customer()
    {
    }

    public Customer(Email email)
    {
        Email = email;
        CustomerStatus = new CustomerStatus(ExpirationDate.Infinite, Status.Regular);
        Role = Role.Regular;
        MoneySpent = Money.Create(0).Value;
    }
    
    public void Update(Email email)
    {
        Email = email;
    }
    
    private bool CanPromote() => 
        !CustomerStatus.IsAdvanced() && HasEnoughMoviesInRecentTime() && MoneySpent.Has(1200);
    
    public Result Promote()
    {
        if (!CanPromote()) return Result.Fail("Customer can't promote!");
        CustomerStatus = new CustomerStatus(new ExpirationDate(DateTime.Now.AddYears(1)), Status.Advanced);
        return Result.Ok();
    }

    public Result PurchaseMovie(Movie movie)
    {
        if(Has(movie)) return Result.Fail("Customer has this movie!");
        var modifier = 1 - CustomerStatus.GetDiscount();
        var price = movie.GetPrice(modifier);
        
        var priceResult = Money.Create(price);
        if (priceResult.IsFailed) return Result.Fail("Error creating money spent!");

        var purchasedMovie = new PurchasedMovie(movie, this);
        _purchasedMovies.Add(purchasedMovie);
        MoneySpent += priceResult.Value;
        return Result.Ok();
    }

    private bool Has(Movie movie) =>
        _purchasedMovies.Any(pm =>
            pm.Movie == movie && !(pm.ExpirationDate.IsExpired()));

    private bool HasEnoughMoviesInRecentTime()
    {
        return _purchasedMovies.Where(movie => movie.PurchaseDate > DateTime.Now.AddMonths(-2)).ToList().Count > 2;
    }
}