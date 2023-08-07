using MovieStore.Core.ValueObjects;

namespace MovieStore.Core.Enum;
public enum LicensingType
{
    TwoDay = 0,
    Lifelong = 1
}

public static class LicensingTypeExtensions
{
    public static decimal GetPrice(this LicensingType type)
    {
        return type switch
        {
            LicensingType.TwoDay => 300,
            LicensingType.Lifelong => 700,
            _ => throw new ArgumentOutOfRangeException(nameof(type))
        };
    }
    public static ExpirationDate GetExpirationDate(this LicensingType type)
    {
        return type switch
        {
            LicensingType.TwoDay => new ExpirationDate(DateTime.Now.AddDays(2)),
            LicensingType.Lifelong => ExpirationDate.Infinite,
            _ => throw new ArgumentOutOfRangeException(nameof(type))
        };
    }
}