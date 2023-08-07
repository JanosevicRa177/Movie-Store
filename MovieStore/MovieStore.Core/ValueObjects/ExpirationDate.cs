using FluentResults;

namespace MovieStore.Core.ValueObjects;

public record ExpirationDate(DateTime Date)
{
    public static ExpirationDate Infinite => new ExpirationDate(DateTime.MaxValue);

    public bool IsExpired() => Date < DateTime.Now;
};