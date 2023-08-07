using System.Reflection;
using Microsoft.EntityFrameworkCore;
using MovieStore.Core.Model;

namespace MovieStore.Infrastructure;

public class MovieStoreContext: DbContext
{
    public MovieStoreContext(DbContextOptions<MovieStoreContext> options) : base(options)
    {
    }

    public DbSet<Customer> Customers { get; set; } = null!;     
    public DbSet<Movie> Movies { get; set; } = null!;
    public DbSet<PurchasedMovie> PurchasedMovies { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}