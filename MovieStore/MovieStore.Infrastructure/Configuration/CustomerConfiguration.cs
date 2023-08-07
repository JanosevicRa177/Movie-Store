using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieStore.Core.Enum;
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

        builder.OwnsOne(x => x.CustomerStatus, customerStatus =>
        {
            customerStatus.Property(x => x.Status)
                .HasColumnName("Status");

            customerStatus.OwnsOne(cs => cs.ExpirationDate)
                .Property(p => p.Date)
                .HasColumnName("StatusExpirationDate");
        });
        
        builder.Property(customer => customer.MoneySpent)
            .HasConversion(moneySpent => moneySpent.Amount,
                money => Money.Create(money).Value);

    }
}