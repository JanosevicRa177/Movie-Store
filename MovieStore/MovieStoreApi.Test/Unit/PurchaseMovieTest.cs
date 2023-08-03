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
public class PurchaseMovieTest
{
    private IRepository<Customer> _customerRepository = null!;
    private IRepository<Movie> _movieRepository = null!;
    private Customer _customer = null!;
    private PurchaseMovie.RequestHandler _handler = null!;

    [SetUp]
    public void Setup()
    {
        _customerRepository = A.Fake<IRepository<Customer>>();
        _movieRepository = A.Fake<IRepository<Movie>>();
        _handler = new PurchaseMovie.RequestHandler(_customerRepository, _movieRepository);

        _customer = new Customer {Email = Email.Create( "email1@gmail.com").Value,Id = Guid.NewGuid(),Status = Status.Regular};
        MockGetById(_customerRepository, _customer);
    }

    [Test]
    public void Purchase_Movie_Success()
    {
        var movie = new Movie { Id = Guid.NewGuid(), Name = "Insidious", LicensingType = LicensingType.Lifelong };
        MockGetById(_movieRepository, movie);
        var command = new PurchaseMovie.Command{CustomerEmail = "email1@gmail.com", MovieId = movie.Id};
        
        var result = Act(command);
        
        result.IsSuccess.Should().Be(true);
    }
    
    [Test]
    public void Purchase_Expired_Movie_Success()
    {
        var movie = new Movie { Id = Guid.NewGuid(), Name = "Insidious", LicensingType = LicensingType.TwoDay };
        MockGetById(_movieRepository, movie);
        var purchasedMovie = new PurchasedMovie {ExpirationDate = DateTime.Now.AddDays(-1),Customer = _customer,Movie = movie};
        _customer.PurchasedMovies.Add(purchasedMovie);
        var command = new PurchaseMovie.Command{CustomerEmail = "email1@gmail.com", MovieId = movie.Id};
        
        var result = Act(command);
        
        result.IsSuccess.Should().Be(true);
    }

    [Test]
    public void Purchase_Movie_Two_Day_Fail()
    {
        var movie = new Movie { Id = Guid.NewGuid(), Name = "Insidious", LicensingType = LicensingType.TwoDay };
        MockGetById(_movieRepository, movie);
        var purchasedMovie = new PurchasedMovie {ExpirationDate = DateTime.Now.AddDays(1),Customer = _customer,Movie = movie};
        _customer.PurchasedMovies.Add(purchasedMovie);
        var command = new PurchaseMovie.Command{CustomerEmail = "email1@gmail.com", MovieId = movie.Id};
        
        var result = Act(command);
        
        result.ErrorShouldHave(StatusCodes.Status400BadRequest);
    }

    [Test]
    public void Purchase_Movie_Lifelong_Fail()
    {
        var movie = new Movie { Id = Guid.NewGuid(), Name = "Insidious", LicensingType = LicensingType.Lifelong };
        MockGetById(_movieRepository, movie);
        var purchasedMovie = new PurchasedMovie {Customer = _customer,Movie = movie};
        _customer.PurchasedMovies.Add(purchasedMovie);
        var command = new PurchaseMovie.Command{CustomerEmail = "email1@gmail.com", MovieId = movie.Id};
        
        var result = Act(command);
        
        result.ErrorShouldHave(StatusCodes.Status400BadRequest);
    }
    [Test]
    public void Movie_Does_Not_Exists_Fail()
    {
        var movieId = Guid.NewGuid();
        A.CallTo(() => _movieRepository.GetById(movieId)).Returns(null);
        var command = new PurchaseMovie.Command{CustomerEmail = "email1@gmail.com", MovieId = movieId};
        
        var result = Act(command);
        
        result.ErrorShouldHave(StatusCodes.Status404NotFound);
    }
    [Test]
    public void Customer_Does_Not_Exists_Fail()
    {
        var email = "something@gmail.com";
        A.CallTo(() => _customerRepository.GetById(new Guid())).Returns(null);
        var movie = new Movie { Id = Guid.NewGuid(), Name = "Insidious", LicensingType = LicensingType.Lifelong };
        MockGetById(_movieRepository, movie);
        var command = new PurchaseMovie.Command{CustomerEmail = email, MovieId = movie.Id};
        
        var result = Act(command);
        
        result.ErrorShouldHave(StatusCodes.Status404NotFound);
    }
    
    private Result Act(PurchaseMovie.Command command) => _handler.Handle(command, new CancellationToken()).Result;
    
    private static void MockGetById(IRepository<Movie> movieRepository,Movie movie1) => 
        A.CallTo(() => movieRepository.GetById(movie1.Id)).Returns(movie1);

    private static void MockGetById(IRepository<Customer> customerRepository,Customer customer) => 
        A.CallTo(() => customerRepository.GetById(customer.Id)).Returns(customer);
}