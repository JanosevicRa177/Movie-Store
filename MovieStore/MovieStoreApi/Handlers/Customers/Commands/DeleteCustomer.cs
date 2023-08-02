using FluentResults;
using MediatR;
using MovieStore.Core.Model;
using MovieStoreApi.Handlers.Http;
using MovieStoreApi.Repositories.Interfaces;

namespace MovieStoreApi.Handlers.Customers.Commands;

public static class DeleteCustomer
{
    public class Command : IRequest<Result> 
    {
        public Guid Id { get; set; }
    }

    public class RequestHandler : IRequestHandler<Command,Result> 
        {
            private readonly ICustomerRepository _customerRepository;
            public RequestHandler(ICustomerRepository customerRepository)
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

                _customerRepository.Delete(customer);
                _customerRepository.SaveChanges();
                
                return HttpHandler.Ok();
            }
        }
    }