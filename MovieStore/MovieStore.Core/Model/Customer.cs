using MovieStore.Core.Enum;
using MovieStore.Core.ValueObjects;

namespace MovieStore.Core.Model;

public class Customer
{
    public Guid Id { get; set; }
    public Email Email { get; set; } = null!;
    public Status Status { get; set; }
    public Role Role { get; set; }
    public DateTime? StatusExpirationDate { get; set; }
    public IList<PurchasedMovie> PurchasedMovies { get; set; } = new List<PurchasedMovie>();
    public double MoneySpent { get; set; }

    public void Update(Email email)
    {
        Email = email;
    }
    public void Upgrade()
    {
        Status = Status.Advanced;
        StatusExpirationDate = DateTime.Now.AddYears(1);
    }
    
    public bool Has(Movie movie) =>
        PurchasedMovies.Any(pm =>
            pm.Movie == movie&& (pm.ExpirationDate > DateTime.Now || pm.ExpirationDate == null));

    public void CalculateAdvanced() => 
        Status = IsAdvanced() ? Status.Advanced : Status.Regular;

    public bool CanUpgrade() => 
        !IsAdvanced() && HasEnoughMoviesInRecentTime() && MoneySpent > 1200;
    
    public void PurchaseMovie(Movie movie)
    {
        double price = movie.LicensingType switch
        {
            LicensingType.Lifelong => 700,
            LicensingType.TwoDay => 300,
            _ => throw new InvalidOperationException()
        };
        if (this.IsAdvanced())
            price *=  0.8;
        MoneySpent += price;
        var purchasedMovie = new PurchasedMovie{Customer = this, Movie = movie,
            PurchaseDate = DateTime.Now, ExpirationDate = movie.LicensingType == LicensingType.TwoDay ? DateTime.Now.AddDays(2): null};
        PurchasedMovies.Add(purchasedMovie);
    }

    private bool HasEnoughMoviesInRecentTime()
    {
        return PurchasedMovies.Where(movie => movie.PurchaseDate > DateTime.Now.AddMonths(-2)).ToList().Count > 2;
    }

    private bool IsAdvanced() => Status == Status.Advanced && StatusExpirationDate > DateTime.Now;
}