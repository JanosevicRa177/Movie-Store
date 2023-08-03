using FakeItEasy;
using FluentAssertions;
using FluentResults;
using Microsoft.AspNetCore.Http;
using MovieStore.Core.Enum;
using MovieStore.Core.Model;
using MovieStoreApi.Handlers.Customers.Commands;
using MovieStoreApi.Repositories.Interfaces;
using MovieStoreApi.Test.Extensions;

namespace MovieStoreApi.Test.Unit;

[TestFixture]
public class UpgradeCustomerTest
{
    private IRepository<Customer> _customerRepository = null!;
    private UpgradeCustomer.RequestHandler _handler = null!;
    private Customer _regularCustomer = null!;

    [SetUp]
    public void Setup()
    {
        _customerRepository = A.Fake<IRepository<Customer>>();
        _handler = new UpgradeCustomer.RequestHandler(_customerRepository);
        _regularCustomer = new Customer {Email = "regular@gmail.com",Id = Guid.NewGuid(),Status = Status.Regular, MoneySpent = 1300}; 
    }

    [Test]
    public void Upgrade_Success()
    {
        MockGetById(_customerRepository, _regularCustomer);
        var lifelongMovie = new Movie { LicensingType = LicensingType.Lifelong,Id = Guid.NewGuid()};
        _regularCustomer.PurchasedMovies.Add(new PurchasedMovie{Movie = lifelongMovie, PurchaseDate = DateTime.Now.AddMonths(-1)});
        _regularCustomer.PurchasedMovies.Add(new PurchasedMovie{Movie = lifelongMovie, PurchaseDate = DateTime.Now.AddMonths(-1)});
        _regularCustomer.PurchasedMovies.Add(new PurchasedMovie{Movie = lifelongMovie, PurchaseDate = DateTime.Now.AddMonths(-1)});
        var command = new UpgradeCustomer.Command{Id = _regularCustomer.Id};
        
        var result = Act(command);
        
        result.IsSuccess.Should().Be(true);
    }
    
    [Test]
    public void Upgrade_Regular_Fail()
    {
        MockGetById(_customerRepository, _regularCustomer);
        var command = new UpgradeCustomer.Command{Id = _regularCustomer.Id};
        
        var result = Act(command);
        
        result.ErrorShouldHave(StatusCodes.Status400BadRequest);
    }

    [Test]
    public void Upgrade_Advanced_Empty_Fail()
    {
        var advancedCustomer = CreateAdvancedCustomerWithExpirationDay(-7); 
        MockGetById(_customerRepository, advancedCustomer);
        var command = new UpgradeCustomer.Command{Id = advancedCustomer.Id};
        
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
        
        var command = new UpgradeCustomer.Command{Id = advancedCustomer.Id};
        
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
        
        var command = new UpgradeCustomer.Command{Id = advancedCustomer.Id};
        
        var result = Act(command);
        
        result.ErrorShouldHave(StatusCodes.Status400BadRequest);
    }

    private static void MockGetById(IRepository<Customer> customerRepository, Customer customer) =>
        A.CallTo(() => customerRepository.GetById(customer.Id)).Returns(customer);
    private Result Act(UpgradeCustomer.Command command) => _handler.Handle(command, new CancellationToken()).Result;
    private static Customer CreateAdvancedCustomerWithExpirationDay(int day) => 
        new Customer {Email = "regular@gmail.com",Id = Guid.NewGuid(),Status = Status.Advanced,StatusExpirationDate = DateTime.Now.AddDays(day)};
}