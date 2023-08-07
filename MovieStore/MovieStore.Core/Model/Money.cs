using FluentResults;

namespace MovieStore.Core.Model;

public record Money
{
    public decimal Amount { get; init; }
    private Money(decimal amount)
    {
        Amount = amount;
    }

    public static Result<Money> Create(decimal value)
    {
        if (value is < 0 or > 100000 || (value != decimal.Round(value,2)))
            return Result.Fail<Money>("Invalid money value!");
        return Result.Ok(new Money(value));
    }
    
    public bool Has(decimal value) => Amount > value;
    
    public static Money operator +(Money money1, Money money2)
    {
        return Money.Create(money1.Amount + money2.Amount).Value;
    }
};