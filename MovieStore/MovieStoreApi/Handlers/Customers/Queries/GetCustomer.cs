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
            if (customer == null) return HttpHandler.NotFound<Response>();
            
            var mapperConfig = new MapperConfiguration(cfg => cfg.CreateMap<Customer, Response>());
            var mapper = new Mapper(mapperConfig);
            var customerRes = mapper.Map<Response>(customer);

            return HttpHandler.Ok(customerRes);
        }
    }
}