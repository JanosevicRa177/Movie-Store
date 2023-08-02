using System.ComponentModel.DataAnnotations;
using AutoMapper;
using FluentResults;
using MediatR;
using MovieStore.Core.Model;
using MovieStoreApi.Handlers.Http;
using MovieStoreApi.Repositories.Interfaces;

namespace MovieStoreApi.Handlers.Movies.Queries;

public static class GetMovies
{
    public class Query : IRequest<Result<IEnumerable<Response>>> { }
    
    public class Response
    {
        [Required]
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
    public class RequestHandler : IRequestHandler<Query, Result<IEnumerable<Response>>>
    {
        private readonly IRepository<Movie> _movieRepository;

        public RequestHandler(IRepository<Movie> movieRepository)
        {
            _movieRepository = movieRepository ?? throw new ArgumentNullException(nameof(movieRepository));
        }

        public Task<Result<IEnumerable<Response>>> Handle(Query request, CancellationToken cancellationToken)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            var movies = _movieRepository.GetAll();
            
            var mapperConfig = new MapperConfiguration(cfg => cfg.CreateMap<Movie, Response>());
            var mapper = new Mapper(mapperConfig);
            var movieDtos = mapper.Map<IEnumerable<Response>>(movies);

            return HttpHandler.Ok(movieDtos);
        }
    }
}