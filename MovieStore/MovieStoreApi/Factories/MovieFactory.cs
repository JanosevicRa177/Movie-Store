using MovieStore.Core.Enum;
using MovieStore.Core.Model;

namespace MovieStoreApi.Factories;

public class MovieFactory
{
    public static Movie? CreateMovie(string name, LicensingType licensingType)
    {
        return licensingType switch
        {
            LicensingType.Lifelong => new LifelongMovie(name),
            LicensingType.TwoDay => new TwoDayMovie(name),
            _ => null
        };
    }
}