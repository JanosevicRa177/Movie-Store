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

}