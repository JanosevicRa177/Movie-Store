using MovieStore.Core.Enum;

namespace MovieStore.Core.Model;

public class Movie
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = null!;
    public LicensingType LicensingType { get; private set; }

    public Movie()
    {
    }

    public Movie(string name, LicensingType licensingType)
    {
        Name = name;
        LicensingType = licensingType;
    }

    public void Update(string name, LicensingType licensingType)
    {
        Name = name;
        LicensingType = licensingType;
    }

}