using MovieStore.Core.Enum;
using MovieStore.Core.ValueObjects;

namespace MovieStore.Core.Model;

public class PurchasedMovie
{

    public Guid Id { get; private set; }
    public DateTime PurchaseDate { get; private set;}
    public ExpirationDate ExpirationDate { get; private set;} = null!;
    public Movie Movie { get;  private set;} = null!;
    public Customer Customer { get; private set;} = null!;

    public PurchasedMovie()
    {
    }

    public PurchasedMovie(Movie movie, Customer customer)
    {
        Movie = movie;
        Customer = customer;
        PurchaseDate = DateTime.Now;
        ExpirationDate = movie.GetExpirationDate();
    }
}