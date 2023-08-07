using FluentResults;
using MovieStore.Core.Enum;

namespace MovieStore.Core.ValueObjects;

public record CustomerStatus()
{
    public ExpirationDate ExpirationDate { get; init; }
    public Status Status { get; init; }
    
    public CustomerStatus(ExpirationDate expirationDate, Status status) : this()
    {
        ExpirationDate = expirationDate;
        Status = status;
    }
    
    public static CustomerStatus Regular => new CustomerStatus(ExpirationDate.Infinite, Status.Regular);

    public bool IsAdvanced() => Status == Enum.Status.Advanced && !ExpirationDate.IsExpired();
};