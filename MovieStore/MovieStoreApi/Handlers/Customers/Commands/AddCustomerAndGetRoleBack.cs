using System.ComponentModel.DataAnnotations;
using FluentResults;
using MediatR;
using MovieStore.Core.Enum;
using MovieStore.Core.Model;
using MovieStore.Core.ValueObjects;
using MovieStoreApi.Repositories.Interfaces;

namespace MovieStoreApi.Handlers.Customers.Commands;

public static class AddCustomerAndGetRoleBack
{
    public class Command : IRequest<Result<Response>>
    {
        public string Email { get; set; } = string.Empty;
    }
    
    public class Response
    {
        [Required]
        public Role Role { get; set; }
    }
    public class RequestHandler : IRequestHandler<Command, Result<Response>>
    {
        private readonly IRepository<Customer> _customerRepository;

        public RequestHandler(IRepository<Customer> customerRepository)
        {
            _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
        }

        public Task<Result<Response>> Handle(Command request, CancellationToken cancellationToken)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));
            
            var emailResult = Email.Create(request.Email);
            if (emailResult.IsFailed)
                return HttpHandler.BadRequest<Response>();
            
            var moneyResult = Money.Create(0);
            if (moneyResult.IsFailed)
                return HttpHandler.BadRequest<Response>();

            var customer = _customerRepository.Search(x => x.Email == emailResult.Value).FirstOrDefault();
            if (customer != null) return HttpHandler.Ok(new Response { Role = customer.Role });

            customer = new Customer(emailResult.Value);
            
            _customerRepository.Add(customer);
            _customerRepository.SaveChanges();
            return HttpHandler.Ok(new Response { Role = customer.Role });
        }
    }
}
