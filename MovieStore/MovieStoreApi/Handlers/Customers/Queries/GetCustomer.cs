using AutoMapper;
using FluentResults;
using MediatR;
using MovieStore.Core.Model;
using MovieStoreApi.Dto;
using MovieStoreApi.Handlers.Http;
using MovieStoreApi.Repositories.Interfaces;

namespace MovieStoreApi.Handlers.Customers.Queries;

public static class GetCustomer
{
    public class Query : IRequest<Result<Response>>
    {
        public Guid Id { get; set; }
    }
    public class Response
    {
        public Guid Id { get; set; }
        public string Email { get; set; } = string.Empty;
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
            {
                throw new ArgumentNullException(nameof(request));
            }

            var customer = _customerRepository.GetById(request.Id);
            return customer != null ? HttpHandler.Ok(new Response{Id = customer.Id,Email = customer.Email}) : HttpHandler.NotFound<Response>();
        }
    }
}