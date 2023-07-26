using MovieStore.Core.Enum;

namespace MovieStore.Core.Model;

public class Movie
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public LicensingType LicensingType { get; set; }

    public void Update(string name, LicensingType licensingType)
    {
        Name = name;
        LicensingType = licensingType;
    }
}