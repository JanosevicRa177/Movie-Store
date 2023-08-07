using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieStore.Core.Enum;
using MovieStore.Core.Model;

namespace MovieStore.Infrastructure.Configuration;

public class MovieConfiguration: IEntityTypeConfiguration<Movie>
{
    public void Configure(EntityTypeBuilder<Movie> builder)
    {
        builder.HasDiscriminator(movie => movie.LicensingType)
            .HasValue<LifelongMovie>(LicensingType.Lifelong)
            .HasValue<TwoDayMovie>(LicensingType.TwoDay);
    }
}