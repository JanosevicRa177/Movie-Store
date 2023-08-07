using System.ComponentModel.DataAnnotations;
using AutoMapper;
using FluentResults;
using JetBrains.Annotations;
using MediatR;
using MovieStore.Core.Enum;
using MovieStore.Core.Model;
using MovieStoreApi.Repositories.Interfaces;

namespace MovieStoreApi.Handlers.Customers.Queries;

public static class GetCustomers
{
    public class Query: IRequest<Result<IEnumerable<Response>>>{ }

    public class Response
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string Email { get; set; } = string.Empty;
        public Status Status { get; set; }
    }

    [UsedImplicitly]
    public class MappingProfile : Profile
    {
        public MappingProfile() => CreateMap<Customer, Response>()
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email.Value))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.CustomerStatus.Status));
    }
    
    public class RequestHandler : IRequestHandler<Query, Result<IEnumerable<Response>>>
    {
        private readonly IRepository<Customer> _customerRepository;
        private readonly IMapper _mapper;
        
        public RequestHandler(IRepository<Customer>customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public Task<Result<IEnumerable<Response>>> Handle(Query request, CancellationToken cancellationToken)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            var customers = _customerRepository.GetAll();

            var customersDto = _mapper.Map<IEnumerable<Response>>(customers);
            
            return Task.FromResult(Result.Ok(customersDto));
        }
    }
}