﻿using FluentResults;
using FluentValidation;
using MediatR;
using MovieStore.Core.Model;
using MovieStore.Core.ValueObjects;
using MovieStoreApi.Repositories.Interfaces;

namespace MovieStoreApi.Handlers.Customers.Commands;

public static class PurchaseMovie
{
    public class Command : IRequest<Result> 
    {
        public string CustomerEmail { get; set; } = null!;
        public Guid MovieId { get; set; }
    }
    
    public class PurchaseMovieCommandValidator : AbstractValidator<Command> 
    {
        public PurchaseMovieCommandValidator()
        {
            RuleFor(x => x.CustomerEmail).EmailAddress();
        }
    }

    public class RequestHandler : IRequestHandler<Command, Result> 
    {
        private readonly IRepository<Customer> _customerRepository;
        private readonly IMovieRepository _movieRepository;
        
        public RequestHandler(IRepository<Customer> customerRepository, IMovieRepository movieRepository)
        {
            _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
            _movieRepository = movieRepository ?? throw new ArgumentNullException(nameof(movieRepository));
        }

        public Task<Result> Handle(Command request, CancellationToken cancellationToken)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));
            
            var emailResult = Email.Create(request.CustomerEmail);
            if (emailResult.IsFailed)
                return HttpHandler.BadRequest("Email not valid!");

            var customer = _customerRepository.Search(x => x.Email == emailResult.Value).FirstOrDefault();
            if (customer == null) return HttpHandler.NotFound("Couldn't find you");
            
            var movie = _movieRepository.GetById(request.MovieId);
            if (movie == null) return HttpHandler.NotFound("This movie does not exists");

            var result = customer.PurchaseMovie(movie);
            if (result.IsFailed) return HttpHandler.BadRequest(result.Errors.First().Message ?? "");
            _customerRepository.SaveChanges();
            
            return HttpHandler.Ok();
        }
    }
    
}