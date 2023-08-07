using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieStore.Core.Model;
using MovieStore.Core.ValueObjects;
using Newtonsoft.Json;

namespace MovieStore.Infrastructure.Configuration;

public class PurchasedMovieConfiguration: IEntityTypeConfiguration<PurchasedMovie>
{
    public void Configure(EntityTypeBuilder<PurchasedMovie> builder)
    {
        builder.HasOne(purchasedMovie => purchasedMovie.Customer);
        builder.HasOne(purchasedMovie => purchasedMovie.Movie);
        
        builder.Property(purchasedMovie => purchasedMovie.ExpirationDate)
            .IsRequired(false)
            .HasConversion(x => x.Date,
                expirationDate => new ExpirationDate(expirationDate));
    }
}