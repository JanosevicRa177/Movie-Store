using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieStore.Core.Model;
using MovieStore.Core.ValueObjects;

namespace MovieStore.Infrastructure.Configuration;

public class CustomerConfiguration: IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder
            .HasMany(customer => customer.PurchasedMovies)
            .WithOne(purchasedMovie => purchasedMovie.Customer);
        builder
            .Property(customer => customer.Email)
            .HasConversion(email => email.Value,
                emailString => Email.Create(emailString).Value);
    }
}