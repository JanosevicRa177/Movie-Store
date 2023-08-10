using System.ComponentModel.DataAnnotations;
using AutoMapper;
using FluentResults;
using FluentValidation;
using JetBrains.Annotations;
using MediatR;
using MovieStore.Core.Model;
using MovieStoreApi.Dto;
using MovieStoreApi.Repositories.Interfaces;

namespace MovieStoreApi.Handlers.Movies.Queries;

public static class GetMovies
{
    public class Query : IRequest<Result<Response>>
    {
        public MoviePaginationDto MoviePaginationDto { get; set; } = null!;
    }
    
    public class UpdateMovieCommandValidator : AbstractValidator<Query> 
    {
        public UpdateMovieCommandValidator()
        {
            RuleFor(x => x.MoviePaginationDto).SetValidator(new MoviePaginationDtoValidator());
        }
    }
    
    public class Response
    {
        [Required]
        public int Size{ get; set; }
        [Required]
        public IEnumerable<MovieDto> Movies { get; set; } = null!;
    }
    
    [UsedImplicitly]
    public class MappingProfile : Profile
    {
        public MappingProfile() => CreateMap<Movie, MovieDto>();
    }
    public class RequestHandler : IRequestHandler<Query, Result<Response>>
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IMapper _mapper;

        public RequestHandler(IMovieRepository movieRepository, IMapper mapper)
        {
            _movieRepository = movieRepository ?? throw new ArgumentNullException(nameof(movieRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public Task<Result<Response>> Handle(Query request, CancellationToken cancellationToken)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            var moviePage = _movieRepository
                .GetPageByLicenseTypeAndName(request.MoviePaginationDto.LicensingType,request.MoviePaginationDto.Name,
                    request.MoviePaginationDto.PageIndex,request.MoviePaginationDto.PageSize);
            
            var movies = _mapper.Map<IEnumerable<MovieDto>>(moviePage.Item1);

            return HttpHandler.Ok(new Response{Movies = movies, Size = moviePage.Item2});
        }
    }
}