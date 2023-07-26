using MovieStore.Core.Enum;

namespace MovieStore.Core.Model;

public class PurchasedMovie
{
    public Guid Id { get; set; }
    public DateTime PurchaseDate { get; set; }
    public DateTime? ExpirationDate { get; set; }
    public Movie Movie { get; set; } = null!;
    public Customer Customer { get; set; } = null!;

    public PurchasedMovie(Customer customer, Movie movie)
    {
        Movie = movie;
        Customer = customer;
        PurchaseDate = DateTime.Now;
        if(movie.LicensingType == LicensingType.TwoDay)
            ExpirationDate = DateTime.Now.AddDays(2);
    }
}