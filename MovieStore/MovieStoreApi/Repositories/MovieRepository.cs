using MovieStore.Core.Model;
using MovieStore.Infrastructure;
using MovieStoreApi.Repositories.Interfaces;

namespace MovieStoreApi.Repositories;

public class MovieRepository: GenericRepository<Movie>
{
    public MovieRepository(MovieStoreContext ctx):base(ctx)
    {
    }
}