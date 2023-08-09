using FluentResults;
using MediatR;
using MovieStore.Core.Enum;
using MovieStore.Core.Model;
using MovieStoreApi.Factories;
using MovieStoreApi.Repositories.Interfaces;

namespace MovieStoreApi.Handlers.Movies.Commands;

public static class AddMovie
{
    public class Command : IRequest<Result> 
    {
        public string Name { get; set; } = string.Empty;
        public LicensingType LicensingType { get; set; }
    }

    public class RequestHandler : IRequestHandler<Command,Result> 
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
            
            var movie = MovieFactory.CreateMovie(request.Name, request.LicensingType);
            if(movie == null) return HttpHandler.BadRequest();
            
            _movieRepository.Add(movie);
            _movieRepository.SaveChanges();
            
            return HttpHandler.Ok();
        }
    }
}