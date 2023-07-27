﻿using FakeItEasy;
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

        _customer = new Customer {Email = "email1@gmail.com",Id = Guid.NewGuid(),Status = Status.Regular};
        MockGetById(_customerRepository, _customer);
    }

    [Test]
    public void Purchase_Movie_Success()
    {
        var movie = new Movie { Id = Guid.NewGuid(), Name = "Insidious", LicensingType = LicensingType.Lifelong };
        MockGetById(_movieRepository, movie);
        var command = new PurchaseMovie.Command{CustomerId = _customer.Id, MovieId = movie.Id};
        
        var result = _handler.Handle(command, new CancellationToken()).ToResult();
        
        result.IsSuccess.Should().Be(true);
    }
    
    [Test]
    public void Purchase_Expired_Movie_Success()
    {
        var movie = new Movie { Id = Guid.NewGuid(), Name = "Insidious", LicensingType = LicensingType.TwoDay };
        MockGetById(_movieRepository, movie);
        var purchasedMovie = new PurchasedMovie {ExpirationDate = DateTime.Now.AddDays(-1),Customer = _customer,Movie = movie};
        _customer.PurchasedMovies.Add(purchasedMovie);
        var command = new PurchaseMovie.Command{CustomerId = _customer.Id, MovieId = movie.Id};
        
        var result = _handler.Handle(command, new CancellationToken()).Result;
        
        result.IsSuccess.Should().Be(true);
    }

    [Test]
    public void Purchase_Movie_Two_Day_Fail()
    {
        var movie = new Movie { Id = Guid.NewGuid(), Name = "Insidious", LicensingType = LicensingType.TwoDay };
        MockGetById(_movieRepository, movie);
        var purchasedMovie = new PurchasedMovie {ExpirationDate = DateTime.Now.AddDays(1),Customer = _customer,Movie = movie};
        _customer.PurchasedMovies.Add(purchasedMovie);
        var command = new PurchaseMovie.Command{CustomerId = _customer.Id, MovieId = movie.Id};
        
        var result = _handler.Handle(command, new CancellationToken()).Result;
        
        result.ErrorShouldHave(StatusCodes.Status400BadRequest);
    }

    [Test]
    public void Purchase_Movie_Lifelong_Fail()
    {
        var movie = new Movie { Id = Guid.NewGuid(), Name = "Insidious", LicensingType = LicensingType.Lifelong };
        MockGetById(_movieRepository, movie);
        var purchasedMovie = new PurchasedMovie {Customer = _customer,Movie = movie};
        _customer.PurchasedMovies.Add(purchasedMovie);
        var command = new PurchaseMovie.Command{CustomerId = _customer.Id, MovieId = movie.Id};
        
        var result = _handler.Handle(command, new CancellationToken()).Result;
        
        result.ErrorShouldHave(StatusCodes.Status400BadRequest);
    }
    [Test]
    public void Movie_Does_Not_Exists_Fail()
    {
        var movieId = Guid.NewGuid();
        A.CallTo(() => _movieRepository.GetById(movieId)).Returns(null);
        var command = new PurchaseMovie.Command{CustomerId = _customer.Id, MovieId = movieId};
        
        var result = _handler.Handle(command, new CancellationToken()).Result;
        
        result.ErrorShouldHave(StatusCodes.Status404NotFound);
    }
    [Test]
    public void Customer_Does_Not_Exists_Fail()
    {
        var customerId = Guid.NewGuid();
        A.CallTo(() => _customerRepository.GetById(customerId)).Returns(null);
        var movie = new Movie { Id = Guid.NewGuid(), Name = "Insidious", LicensingType = LicensingType.Lifelong };
        MockGetById(_movieRepository, movie);
        var command = new PurchaseMovie.Command{CustomerId = customerId, MovieId = movie.Id};
        
        var result = _handler.Handle(command, new CancellationToken()).Result;
        
        result.ErrorShouldHave(StatusCodes.Status404NotFound);
    }
    
    private void MockGetById(IRepository<Movie> movieRepository,Movie movie1)
    {
        A.CallTo(() => movieRepository.GetById(movie1.Id)).Returns(movie1);
    }
    private void MockGetById(IRepository<Customer> customerRepository,Customer customer)
    {
        A.CallTo(() => customerRepository.GetById(customer.Id)).Returns(customer);
    }
}