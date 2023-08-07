using FluentAssertions;
using MovieStore.Core.Model;

namespace MovieStoreApi.Test.Unit;

[TestFixture]
public class MoneyTest
{
    [Test]
    public void Money_Success()
    {
        var result = Money.Create(1000);
        
        result.IsSuccess.Should().Be(true);
    }
    
    [Test]
    public void Money_Negative_Fail()
    {
        var result = Money.Create(-100);
        
        result.IsFailed.Should().Be(true);
    }
    
    [Test]
    public void Money_Too_Big_Fail()
    {
        var result = Money.Create(100000000);
        
        result.IsFailed.Should().Be(true);
    }
    
    [Test]
    public void Money_Has_Too_Many_Decimals_Fail()
    {
        var result = Money.Create(3.141592M);
        
        result.IsFailed.Should().Be(true);
    }
}