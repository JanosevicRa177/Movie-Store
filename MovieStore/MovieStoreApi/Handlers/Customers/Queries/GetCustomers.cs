using AutoMapper;
using FluentResults;
using MediatR;
using MovieStore.Core.Model;
using MovieStoreApi.Dto;
using MovieStoreApi.Repositories.Interfaces;

namespace MovieStoreApi.Handlers.Customers.Queries;

public static class GetCustomers
{
    public class Query: IRequest<Result<IEnumerable<Response>>>{ }

    public class Response
    {
        public Guid Id { get; set; }
        public string Email { get; set; } = string.Empty;
    }

    public class RequestHandler : IRequestHandler<Query, Result<IEnumerable<Response>>>
    {
        private readonly IRepository<Customer> _customerRepository;

        public RequestHandler(IRepository<Customer> customerRepository)
        {
            _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
        }

        public Task<Result<IEnumerable<Response>>> Handle(Query request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }
            
            var customers = _customerRepository.GetAll();
            
            var mapperConfig = new MapperConfiguration(cfg => cfg.CreateMap<Customer, Response>());
            var mapper = new Mapper(mapperConfig);
            var customersDto = mapper.Map<IEnumerable<Response>>(customers);
            
            return Task.FromResult(Result.Ok(customersDto));
        }
    }
}