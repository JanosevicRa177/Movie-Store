using FluentResults;
using MediatR;
using MovieStore.Core.Model;
using MovieStoreApi.Handlers.Http;
using MovieStoreApi.Repositories.Interfaces;

namespace MovieStoreApi.Handlers.Customers.Commands;

public static class PurchaseMovie
{
    public class Command : IRequest<Result> 
    {
        public string CustomerEmail { get; set; } = null!;
        public Guid MovieId { get; set; }
    }

    public class RequestHandler : IRequestHandler<Command, Result> 
    {
        private readonly IRepository<Customer> _customerRepository;
        private readonly IRepository<Movie> _movieRepository;
        
        public RequestHandler(IRepository<Customer> customerRepository, IRepository<Movie> movieRepository)
        {
            _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
            _movieRepository = movieRepository ?? throw new ArgumentNullException(nameof(movieRepository));
        }

        public Task<Result> Handle(Command request, CancellationToken cancellationToken)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            var customer = _customerRepository.Search(x => x.Email == request.CustomerEmail).FirstOrDefault();
            if (customer == null) return HttpHandler.NotFound();
            
            var movie = _movieRepository.GetById(request.MovieId);
            if (movie == null) return HttpHandler.NotFound();

            if (customer.Has(movie)) return HttpHandler.BadRequest();
            
            customer.PurchaseMovie(movie);
            _customerRepository.SaveChanges();
            
            return HttpHandler.Ok();
        }
    }
    
}