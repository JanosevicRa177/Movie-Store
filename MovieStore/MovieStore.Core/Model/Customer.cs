﻿using MovieStore.Core.Enum;

namespace MovieStore.Core.Model;

public class Customer
{
    public Guid Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public Status Status { get; set; }
    public Role Role { get; set; }
    public DateTime? StatusExpirationDate { get; set; }
    public IList<PurchasedMovie> PurchasedMovies { get; set; } = new List<PurchasedMovie>();

    public void Update( string email)
    {
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
    
    public bool Has(Movie movie)
    {
        return PurchasedMovies.Any(pm => pm.Movie == movie && pm.ExpirationDate > DateTime.Now);
    }

    private bool IsAdvanced()
    {
        return Status == Status.Advanced && StatusExpirationDate > DateTime.Now.AddMonths(-1);
    }

    public void PurchaseMovie(Movie movie)
    {
        var purchasedMovie = new PurchasedMovie{Customer = this, Movie = movie,
            PurchaseDate = DateTime.Now, ExpirationDate = movie.LicensingType == LicensingType.TwoDay ? DateTime.Now.AddDays(2): null};
        PurchasedMovies.Add(purchasedMovie);
    }
    
}