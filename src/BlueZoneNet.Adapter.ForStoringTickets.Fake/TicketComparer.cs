using BlueZoneNet.Hexagon.Ports.Driven.ForStoringTickets;

namespace BlueZoneNet.Adapter.ForStoringTickets.Fake;

public class TicketComparer : IComparer<Ticket>
{
    public int Compare(Ticket? ticket1, Ticket? ticket2)
    {
        if (ticket1 is null || ticket2 is null)
        {
            return 0;
        }

        if (object.ReferenceEquals(ticket1, ticket2))
        {
            return 0;
        }

        DateTime endingDateTime1 = ticket1.EndingDateTime;
        DateTime endingDateTime2 = ticket2.EndingDateTime;

        if (endingDateTime1 > endingDateTime2)
        {
            return -1;
        }

        if (endingDateTime1 < endingDateTime2)
        {
            return 1;
        }

        return 0;
    }
}