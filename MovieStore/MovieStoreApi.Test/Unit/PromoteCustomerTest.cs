using System.Linq.Expressions;
using FakeItEasy;
using FluentAssertions;
using FluentResults;
using Microsoft.AspNetCore.Http;
using MovieStore.Core.Enum;
using MovieStore.Core.Model;
using MovieStore.Core.ValueObjects;
using MovieStoreApi.Handlers.Customers.Commands;
using MovieStoreApi.Repositories.Interfaces;
using MovieStoreApi.Test.Extensions;

namespace MovieStoreApi.Test.Unit;

[TestFixture]
public class PromoteCustomerTest
{
    private IRepository<Customer> _customerRepository = null!;
    private PromoteCustomer.RequestHandler _handler = null!;

    private static Customer RegularCustomer => new Customer
    {
        Email = Email.Create("regular@gmail.com").Value, Id = Guid.NewGuid(),
        CustomerStatus = new CustomerStatus(ExpirationDate.Infinite, Status.Regular),
        MoneySpent = Money.Create(1300M).Value
    };

    [SetUp]
    public void Setup()
    {
        _customerRepository = A.Fake<IRepository<Customer>>();
        _handler = new PromoteCustomer.RequestHandler(_customerRepository);
    }

    [Test]
    public void Upgrade_Success()
    {
        MockGetById(_customerRepository, RegularCustomer);
        var lifelongMovie = new Movie { LicensingType = LicensingType.Lifelong,Id = Guid.NewGuid()};
        RegularCustomer.PurchasedMovies.Add(new PurchasedMovie{Movie = lifelongMovie, PurchaseDate = DateTime.Now.AddMonths(-1)});
        RegularCustomer.PurchasedMovies.Add(new PurchasedMovie{Movie = lifelongMovie, PurchaseDate = DateTime.Now.AddMonths(-1)});
        RegularCustomer.PurchasedMovies.Add(new PurchasedMovie{Movie = lifelongMovie, PurchaseDate = DateTime.Now.AddMonths(-1)});
        var command = new PromoteCustomer.Command{Id = RegularCustomer.Id};
        
        var result = Act(command);
        
        result.IsSuccess.Should().Be(true);
    }
    
    [Test]
    public void Upgrade_Regular_Fail()
    {
        MockGetById(_customerRepository, RegularCustomer);
        var command = new PromoteCustomer.Command{Id = RegularCustomer.Id};
        
        var result = Act(command);
        
        result.ErrorShouldHave(StatusCodes.Status400BadRequest);
    }

    [Test]
    public void Upgrade_Advanced_Empty_Fail()
    {
        var advancedCustomer = CreateAdvancedCustomerWithExpirationDay(-7); 
        MockGetById(_customerRepository, advancedCustomer);
        var command = new PromoteCustomer.Command{Id = advancedCustomer.Id};
        
        var result = Act(command);
        
        result.ErrorShouldHave(StatusCodes.Status400BadRequest);
    }

    [Test]
    public void Upgrade_Advanced_Populated_Fail()
    {
        var advancedCustomer = CreateAdvancedCustomerWithExpirationDay(7); 
        MockGetById(_customerRepository, advancedCustomer);
        var lifelongMovie = new Movie { LicensingType = LicensingType.Lifelong,Id = Guid.NewGuid()};
        advancedCustomer.PurchasedMovies.Add(new PurchasedMovie{Movie = lifelongMovie, PurchaseDate = DateTime.Now.AddMonths(-1)});
        advancedCustomer.PurchasedMovies.Add(new PurchasedMovie{Movie = lifelongMovie, PurchaseDate = DateTime.Now.AddMonths(-1)});
        advancedCustomer.PurchasedMovies.Add(new PurchasedMovie{Movie = lifelongMovie, PurchaseDate = DateTime.Now.AddMonths(-1)});
        
        var command = new PromoteCustomer.Command{Id = advancedCustomer.Id};
        
        var result = Act(command);
        
        result.ErrorShouldHave(StatusCodes.Status400BadRequest);
    }


    [Test]
    public void Upgrade_Regular_Populated_With_Expired_Movies_Fail()
    {
        var advancedCustomer = CreateAdvancedCustomerWithExpirationDay(7);  
        MockGetById(_customerRepository, advancedCustomer);
        var twoDayMovie = new Movie { LicensingType = LicensingType.TwoDay,Id = Guid.NewGuid()};
        advancedCustomer.PurchasedMovies.Add(new PurchasedMovie{Movie = twoDayMovie, PurchaseDate = DateTime.Now.AddMonths(-3)});
        advancedCustomer.PurchasedMovies.Add(new PurchasedMovie{Movie = twoDayMovie, PurchaseDate = DateTime.Now.AddMonths(-3)});
        advancedCustomer.PurchasedMovies.Add(new PurchasedMovie{Movie = twoDayMovie, PurchaseDate = DateTime.Now.AddMonths(-3)});
        
        var command = new PromoteCustomer.Command{Id = advancedCustomer.Id};
        
        var result = Act(command);
        
        result.ErrorShouldHave(StatusCodes.Status400BadRequest);
    }

    private Result Act(PromoteCustomer.Command command) => _handler.Handle(command, new CancellationToken()).Result;
    
    private static void MockGetById(IRepository<Customer> customerRepository,Customer customer) => 
        A.CallTo(() => customerRepository.GetById(customer.Id)).Returns(customer);
    private static Customer CreateAdvancedCustomerWithExpirationDay(int day) {}
        new Customer {Email = Email.Create("advanced@gmail.com").Value,Id = Guid.NewGuid(),CustomerStatus = new CustomerStatus(new ExpirationDate(DateTime.Now.AddDays(day)),Status.Advanced)};
}