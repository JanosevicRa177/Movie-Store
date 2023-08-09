using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieStore.Core.Model;

namespace MovieStore.Infrastructure.Configuration;

public class PurchasedMovieConfiguration : IEntityTypeConfiguration<PurchasedMovie>
{
    public void Configure(EntityTypeBuilder<PurchasedMovie> builder)
    {
        builder.HasOne(purchasedMovie => purchasedMovie.Customer);
        builder.HasOne(purchasedMovie => purchasedMovie.Movie);

        builder.OwnsOne(x => x.ExpirationDate, expirationDate =>
        {
            expirationDate.Property(x => x.Date)
                .HasColumnName("ExpirationDate");
        });
    }
}