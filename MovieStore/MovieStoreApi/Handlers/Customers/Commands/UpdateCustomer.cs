using FluentResults;
using MediatR;
using MovieStore.Core.Model;
using MovieStore.Core.ValueObjects;
using MovieStoreApi.Repositories.Interfaces;

namespace MovieStoreApi.Handlers.Customers.Commands;

public static class UpdateCustomer
{
    public class Command : IRequest<Result> 
    {
        public Guid Id { get; set; }
        public string Email { get; set; } = null!;
    }
    
    public class RequestHandler : IRequestHandler<Command,Result> 
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
            
            var emailResult = Email.Create(request.Email);
            if (emailResult.IsFailed)
                return HttpHandler.BadRequest();

            customer.Update(emailResult.Value);
            _customerRepository.SaveChanges();
            
            return  HttpHandler.Ok();

        }
    }
}