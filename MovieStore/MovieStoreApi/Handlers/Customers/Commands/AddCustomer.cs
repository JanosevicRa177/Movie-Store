﻿using FluentResults;
using MediatR;
using MovieStore.Core.Enum;
using MovieStore.Core.Model;
using MovieStoreApi.Handlers.Http;
using MovieStoreApi.Repositories.Interfaces;

namespace MovieStoreApi.Handlers.Customers.Commands;

public static class AddCustomer
{
    public class Command : IRequest<Result> 
    {
        public string Email { get; set; } = null!;
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
            var customer = new Customer
                { Email = request.Email, Role = Role.Regular, Status = Status.Regular };
            _customerRepository.Add(customer);
            _customerRepository.SaveChanges();

            return HttpHandler.Ok();
        }
    }
}
