using MovieStore.Core.Enum;
using MovieStore.Core.ValueObjects;

namespace MovieStore.Core.Model;

public class LifelongMovie: Movie
{
    public LifelongMovie(string name) : base(name)
    {
        LicensingType = LicensingType.Lifelong;
    }
    protected override decimal GetBasePrice() => 700M;
    public override ExpirationDate GetExpirationDate() => ExpirationDate.Infinite;
}