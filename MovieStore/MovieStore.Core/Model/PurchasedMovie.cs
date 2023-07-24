namespace MovieStore.Core.Model;

public class PurchasedMovie
{
    public Guid Id { get; set; }
    public DateTime PurchaseDate { get; set; }
    public DateTime? ExpirationDate { get; set; }
    public Movie Movie { get; set; } = null!;
    public Customer Customer { get; set; } = null!;
}