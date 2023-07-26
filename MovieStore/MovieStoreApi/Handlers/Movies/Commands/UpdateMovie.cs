﻿using FluentResults;
using MediatR;
using MovieStore.Core.Enum;
using MovieStore.Core.Model;
using MovieStoreApi.Errors;
using MovieStoreApi.Handlers.Http;
using MovieStoreApi.Repositories.Interfaces;

namespace MovieStoreApi.Movies.Commands;

public abstract class UpdateMovie
{
    public class Command : IRequest<Result>
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public LicensingType LicensingType { get; set; }
    }

    public class RequestHandler : IRequestHandler<Command, Result>
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

            var movie = _movieRepository.GetById(request.Id);
            if (movie == null)
                return HttpHandler.NotFound();

            movie.Update( request.Name, request.LicensingType);
            _movieRepository.SaveChanges();
            return HttpHandler.Ok();
        }
    }
}