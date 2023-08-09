using MovieStore.Core.Enum;
using MovieStore.Core.Model;
using MovieStore.Infrastructure;
using MovieStoreApi.Repositories.Interfaces;

namespace MovieStoreApi.Repositories;

public class MovieRepository: GenericRepository<Movie>, IMovieRepository
{
    public MovieRepository(MovieStoreContext ctx):base(ctx)
    {
    }

    public (IEnumerable<Movie>, int) GetPageByLicenseTypeAndName(LicensingType? licensingType, string? name, int pageIndex, int pageSize)
    {
        IQueryable<Movie> query = Context.Set<Movie>();

        if (!string.IsNullOrEmpty(name))
            query = query.Where(m => m.Name.Contains(name));

        if (licensingType != null)
            query = query.Where(m => m.LicensingType == licensingType);

        var totalCount = query.Count();

        query = query
            .OrderBy(m => m.Name)
            .Skip((pageIndex) * pageSize)
            .Take(pageSize);

        return (query.AsEnumerable(), totalCount);
    }
}