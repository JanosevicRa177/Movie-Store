using FluentResults;
using MediatR;
using MovieStore.Core.Enum;
using MovieStore.Core.Model;
using MovieStoreApi.Handlers.Http;
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
        private readonly IRepository<Movie> _movieRepository;
        public RequestHandler(IRepository<Movie> movieRepository)
        {
            _movieRepository = movieRepository ?? throw new ArgumentNullException(nameof(movieRepository));
        }

        public Task<Result> Handle(Command request, CancellationToken cancellationToken)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            _movieRepository.Add(new Movie{Name = request.Name,LicensingType = request.LicensingType});
            _movieRepository.SaveChanges();
            
            return HttpHandler.Ok();
        }
    }
}