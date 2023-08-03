using System.ComponentModel.DataAnnotations;
using FluentResults;
using MediatR;
using MovieStore.Core.Enum;
using MovieStore.Core.Model;
using MovieStoreApi.Handlers.Http;
using MovieStoreApi.Repositories.Interfaces;

namespace MovieStoreApi.Handlers.Customers.Queries;

public static class GetCurrentCustomerRole
{
    public class Query : IRequest<Result<Response>>
    {
        public string Email { get; set; } = null!;
    }
    
    public class Response
    {
        [Required]
        public Role Role { get; set; }
    }
    public class RequestHandler : IRequestHandler<Query, Result<Response>>
    {
        private readonly IRepository<Customer> _customerRepository;

        public RequestHandler(IRepository<Customer> customerRepository)
        {
            _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
        }

        public Task<Result<Response>> Handle(Query request, CancellationToken cancellationToken)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            var customer = _customerRepository.Search(x => x.Email == request.Email).FirstOrDefault();
            if (customer != null) return HttpHandler.Ok(new Response { Role = customer.Role });
            
            customer = new Customer
                { Email = request.Email, Role = Role.Regular, Status = Status.Regular };
            _customerRepository.Add(customer);
            _customerRepository.SaveChanges();
            return HttpHandler.Ok(new Response { Role = customer.Role });
        }
    }
}