using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieStore.Core.Model;

namespace MovieStore.Infrastructure.Configuration;

public class CustomerConfiguration: IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.Ignore(customer => customer.Status)
            .HasMany(customer => customer.PurchasedMovies)
            .WithOne(purchasedMovie => purchasedMovie.Customer);
    }
}