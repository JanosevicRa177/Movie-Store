using FluentResults;
using MediatR;
using MovieStore.Core.Enum;
using MovieStore.Core.Model;
using MovieStoreApi.Repositories.Interfaces;

namespace MovieStoreApi.Handlers.Movies.Commands;

public abstract class UpdateMovie
{
    public class Command : IRequest<Result>
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public LicensingType LicensingType { get; set; }
    }

    public class RequestHandler : IRequestHandler<Command, Result>
    {
        private readonly IMovieRepository _movieRepository;
        public RequestHandler(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository ?? throw new ArgumentNullException(nameof(movieRepository));
        }

        public Task<Result> Handle(Command request, CancellationToken cancellationToken)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            var movie = _movieRepository.GetById(request.Id);
            if (movie == null)
                return HttpHandler.NotFound();

            movie.Update( request.Name, request.LicensingType);
            _movieRepository.SaveChanges();
            return HttpHandler.Ok();
        }
    }
}