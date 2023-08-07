namespace MovieStore.Core.Enum;

public enum Status
{
    Regular = 0,
    Advanced = 1
}

public static class StatusExtensions
{
    public static decimal GetDiscount(this Status status)
    {
        return status switch
        {
            Status.Regular => 1M,
            Status.Advanced => 0.8M,
            _ => throw new ArgumentOutOfRangeException(nameof(status))
        };
    }
}