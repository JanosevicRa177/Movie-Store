using FluentResults;
using MediatR;
using MovieStore.Core.Model;
using MovieStoreApi.Handlers.Http;
using MovieStoreApi.Repositories.Interfaces;

namespace MovieStoreApi.Handlers.Movies.Queries;

public static class GetMovie
{
    public class Query : IRequest<Result<Response>>
    {
        public Guid Id { get; set; }
    }
    public class Response
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }

    public class RequestHandler : IRequestHandler<Query, Result<Response>>
    {
        private readonly IRepository<Movie> _movieRepository;

        public RequestHandler(IRepository<Movie> movieRepository)
        {
            _movieRepository = movieRepository ?? throw new ArgumentNullException(nameof(movieRepository));
        }

        public Task<Result<Response>> Handle(Query request, CancellationToken cancellationToken)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            var movie = _movieRepository.GetById(request.Id);
            return movie != null ? HttpHandler.Ok(new Response{Id = movie.Id,Name = movie.Name}) : HttpHandler.NotFound<Response>();
        }
    }
}