using System.Diagnostics.CodeAnalysis;
using BlueZoneNet.Hexagon.Ports.Driven.ForStoringTickets;

namespace BlueZoneNet.Driver.ForParkingCars.Test;

public class TicketComparer : IEqualityComparer<Ticket>
{
    public bool Equals(Ticket? ticket1, Ticket? ticket2)
    {
        if (ticket1 is null || ticket2 is null)
        {
            return false;
        }

        if (object.ReferenceEquals(ticket1, ticket2))
        {
            return true;
        }

        return ticket1.Code == ticket2.Code && ticket1.CarPlate == ticket2.CarPlate;
    }

    public int GetHashCode([DisallowNull] Ticket obj)
    {
        return obj.GetHashCode();
    }
}
