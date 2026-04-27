using System.Diagnostics.CodeAnalysis;
using BlueZoneNet.Hexagon.Ports.Driven.ForPaying;

namespace BlueZoneNet.Driver.ForParkingCars.Test;

public class PayRequestComparer : IEqualityComparer<PayRequest>
{
    public bool Equals(PayRequest? payRequest1, PayRequest? payRequest2)
    {
        if (payRequest1 is null || payRequest2 is null)
        {
            return false;
        }

        if (object.ReferenceEquals(payRequest1, payRequest2))
        {
            return true;
        }

        return payRequest1.TicketCode == payRequest2.TicketCode && payRequest1.PaymentCard == payRequest2.PaymentCard;
    }

    public int GetHashCode([DisallowNull] PayRequest obj)
    {
        return obj.GetHashCode();
    }
}
