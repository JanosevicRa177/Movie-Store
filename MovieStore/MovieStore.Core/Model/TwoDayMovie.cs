using MovieStore.Core.Enum;
using MovieStore.Core.ValueObjects;

namespace MovieStore.Core.Model;

public class TwoDayMovie :Movie
{
    public TwoDayMovie(string name) : base(name)
    {
        LicensingType = LicensingType.TwoDay;
    }
    protected override decimal GetBasePrice() => 300M;
    public override ExpirationDate GetExpirationDate() => new ExpirationDate(DateTime.Now.AddDays(2));
}