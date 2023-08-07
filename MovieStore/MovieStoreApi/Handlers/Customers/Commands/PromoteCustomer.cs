using FluentResults;
using MediatR;
using Microsoft.IdentityModel.Tokens;
using MovieStore.Core.Enum;
using MovieStore.Core.Model;
using MovieStoreApi.Repositories.Interfaces;

namespace MovieStoreApi.Handlers.Customers.Commands;

public static class PromoteCustomer
{
    public class Command : IRequest<Result> 
    {
        public Guid Id { get; set; }
    }
    
    public class RequestHandler : IRequestHandler<Command, Result> 
    {
        private readonly IRepository<Customer> _customerRepository;
        
        public RequestHandler(IRepository<Customer> customerRepository)
        {
            _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
        }

        public Task<Result> Handle(Command request, CancellationToken cancellationToken)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            var customer = _customerRepository.GetById(request.Id);
            if (customer == null)
                return HttpHandler.NotFound();
            var result = customer.Promote();
            if(result.IsFailed)  return HttpHandler.BadRequest();
            _customerRepository.SaveChanges();

            return HttpHandler.Ok();

        }
    }
}