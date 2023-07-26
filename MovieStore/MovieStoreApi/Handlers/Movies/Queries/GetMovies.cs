using System.Collections;
using FluentResults;
using MediatR;
using MovieStore.Core.Model;
using MovieStoreApi.Handlers.Http;
using MovieStoreApi.Repositories.Interfaces;

namespace MovieStoreApi.Handlers.Movies.Queries;

public static class GetMovies
{
    public class Query : IRequest<Result<IEnumerable<Movie>>> { }
    public class RequestHandler : IRequestHandler<Query, Result<IEnumerable<Movie>>>
    {
        private readonly IRepository<Movie> _movieRepository;

        public RequestHandler(IRepository<Movie> movieRepository)
        {
            _movieRepository = movieRepository ?? throw new ArgumentNullException(nameof(movieRepository));
        }

        public Task<Result<IEnumerable<Movie>>> Handle(Query request, CancellationToken cancellationToken)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            var movies = _movieRepository.GetAll();

            return HttpHandler.Ok(movies);
        }
    }
}