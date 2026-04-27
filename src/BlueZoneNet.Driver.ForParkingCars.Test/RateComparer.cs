using System.Diagnostics.CodeAnalysis;
using BlueZoneNet.Hexagon.Ports.Driven.ForObtainingRates;

namespace BlueZoneNet.Driver.ForParkingCars.Test;

public class RateEqualityComparer : IEqualityComparer<Rate>
{
    public bool Equals(Rate? rate1, Rate? rate2)
    {
        if (rate1 is null || rate2 is null)
        {
            return false;
        }

        if (object.ReferenceEquals(rate1, rate2))
        {
            return true;
        }

        return rate1.AmountPerHour == rate2.AmountPerHour && 
            rate1.Name == rate2.Name;
    }

    public int GetHashCode([DisallowNull] Rate obj)
    {
        return obj.GetHashCode();
    }
}