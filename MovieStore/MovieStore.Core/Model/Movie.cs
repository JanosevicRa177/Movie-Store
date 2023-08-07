using MovieStore.Core.Enum;
using MovieStore.Core.ValueObjects;

namespace MovieStore.Core.Model;

public abstract class Movie
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = null!;
    public LicensingType LicensingType { get; protected set; }

    public Movie()
    {
    }

    protected Movie(string name)
    {
        Name = name;
    }

    public void Update(string name, LicensingType licensingType)
    {
        Name = name;
        LicensingType = licensingType;
    }

    protected abstract decimal GetBasePrice();

    public decimal GetPrice(decimal modifier) => modifier * GetBasePrice();
    public abstract ExpirationDate GetExpirationDate();

}