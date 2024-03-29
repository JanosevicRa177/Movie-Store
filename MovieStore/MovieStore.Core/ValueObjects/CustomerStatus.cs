﻿using MovieStore.Core.Enum;

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

    public bool IsAdvanced() => Status == Status.Advanced && !ExpirationDate.IsExpired();

    public decimal GetDiscount() => IsAdvanced() ? 0.2m : 0m;
};