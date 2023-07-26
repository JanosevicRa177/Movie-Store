using FluentResults;
using MediatR;
using Microsoft.IdentityModel.Tokens;
using MovieStore.Core.Enum;
using MovieStore.Core.Model;
using MovieStoreApi.Handlers.Http;
using MovieStoreApi.Repositories.Interfaces;

namespace MovieStoreApi.Handlers.Customers.Commands;

public static class UpgradeCustomer
{
    public class Command : IRequest<Result> 
    {
        public Guid Id { get; set; }
    }
    
    public class RequestHandler : IRequestHandler<Command, Result> 
    {
        private readonly IRepository<Customer> _customerRepository;
        private readonly IRepository<PurchasedMovie> _purchasedMovieRepository;
        
        public RequestHandler(IRepository<Customer> customerRepository, IRepository<PurchasedMovie> purchasedMovieRepository)
        {
            _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
            _purchasedMovieRepository = purchasedMovieRepository ?? throw new ArgumentNullException(nameof(purchasedMovieRepository));
        }

        public Task<Result> Handle(Command request, CancellationToken cancellationToken)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            var customer = _customerRepository.GetById(request.Id);
            if (customer == null)
                return HttpHandler.NotFound();

            var purchasedMovies = _purchasedMovieRepository.Search(movie => 
                movie.Customer == customer && movie.PurchaseDate > DateTime.Now.AddMonths(-2));
            
            if (purchasedMovies.ToList().Count < 2 || customer.Status == Status.Advanced)
                return  HttpHandler.BadRequest();
            
            customer.Upgrade();
            _customerRepository.SaveChanges();

            return HttpHandler.Ok();

        }
    }
}