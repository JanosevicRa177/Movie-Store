using MovieStore.Core.Enum;

namespace MovieStore.Core.Model;

public class Customer
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public Status Status { get; set; }
    public Role Role { get; set; }
    public DateTime? StatusExpirationDate { get; set; }
    public IList<PurchasedMovie> PurchasedMovies { get; set; } = new List<PurchasedMovie>();

    public void Update(string name, string email)
    {
        Name = name;
        Email = email;
    }
    public void Upgrade()
    {
        Status = Status.Advanced;
        StatusExpirationDate = DateTime.Now.AddMonths(1);
    }

    public bool CanUpgrade()
    {
        var purchasedMovies = PurchasedMovies.Where(pm => pm.PurchaseDate > DateTime.Now.AddMonths(-2));
        return purchasedMovies.ToList().Count > 2 && !IsAdvanced();
    }

    private bool IsAdvanced()
    {
        return Status == Status.Advanced && StatusExpirationDate > DateTime.Now.AddMonths(-1);
    }

    public void PurchaseMovie(Movie movie)
    {
        var purchasedMovie = new PurchasedMovie(this, movie);
        PurchasedMovies.Add(purchasedMovie);
    }
    
}