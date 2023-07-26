using FluentResults;
using MediatR;
using MovieStore.Core.Model;
using MovieStoreApi.Errors;
using MovieStoreApi.Handlers.Http;
using MovieStoreApi.Repositories.Interfaces;

namespace MovieStoreApi.Movies.Queries;

public static class GetMovie
{
    public class Query : IRequest<Result<Movie>>
    {
        public Guid Id { get; set; }
    }

    public class Response
    {
    }

    public class RequestHandler : IRequestHandler<Query, Result<Movie>>
    {
        private readonly IRepository<Movie> _movieRepository;

        public RequestHandler(IRepository<Movie> movieRepository)
        {
            _movieRepository = movieRepository ?? throw new ArgumentNullException(nameof(movieRepository));
        }

        public Task<Result<Movie>> Handle(Query request, CancellationToken cancellationToken)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            var movie = _movieRepository.GetById(request.Id);
            return movie != null ? HttpHandler.Ok(movie) : HttpHandler.NotFound<Movie>();
        }
    }
}