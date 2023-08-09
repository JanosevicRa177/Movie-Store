using MovieStore.Core.Enum;
using MovieStore.Core.Model;

namespace MovieStoreApi.Repositories.Interfaces;

public interface IMovieRepository: IRepository<Movie>
{
    (IEnumerable<Movie>,int) GetPageByLicenseTypeAndName(LicensingType? licensingType, string? name, int pageIndex, int pageSize);
}