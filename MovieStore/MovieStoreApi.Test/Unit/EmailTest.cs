using FluentAssertions;
using MovieStore.Core.ValueObjects;
using NUnit.Framework;

namespace MovieStoreApi.Test.Unit;

[TestFixture]
public class EmailTest
{
    [Test]
    public void Email_Success()
    {
        var result = Email.Create("email@gmail.com");
        
        result.IsSuccess.Should().Be(true);
    }
    
    [Test]
    public void Email_Success_advanced()
    {
        var result = Email.Create("email@uns.ac.rs");
        
        result.IsSuccess.Should().Be(true);
    }
    
    [Test]
    public void Email_Invalid_1()
    {
        var result = Email.Create("emailgmail.com");
        
        result.IsFailed.Should().Be(true);
    }
    
    [Test]
    public void Email_Invalid_2()
    {
        var result = Email.Create("email@gmcom");
        
        result.IsFailed.Should().Be(true);
    }
    
    [Test]
    public void Email_Invalid_3()
    {
        var result = Email.Create("@gmail.com");
        
        result.IsFailed.Should().Be(true);
    }
}