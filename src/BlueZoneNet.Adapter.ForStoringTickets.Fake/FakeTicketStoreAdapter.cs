using BlueZoneNet.Hexagon.Ports.Driven.ForStoringTickets;

namespace BlueZoneNet.Adapter.ForStoringTickets.Fake;

/**
 * Driven adapter that implements "forstoringtickets" port with a fake (in-memory db) test double.
 */
public class FakeTicketStoreAdapter : IForStoringTickets
{
    private const int MaxTicketCodeLength = 10;
    private Dictionary<string,Ticket> ticketsByCode;
	private long value;

    public FakeTicketStoreAdapter()
    {
        this.ticketsByCode = new Dictionary<string, Ticket>();
        SetNextCode("1");
    }

    public string NextCode()
    {
        string currentTicketCodeAndIncrement  = value++.ToString();
        return LeftPaddedTicketCode(currentTicketCodeAndIncrement);
    }

    public void SetNextCode (string ticketCode)
    {
        if (ticketCode.Length > MaxTicketCodeLength)
        {
            throw new Exception("Ticket code overflow");
        }

        long codeAsLong = long.Parse(ticketCode);
        this.value = codeAsLong;
    }

    public string NextAvailableCode()
    {
        string currentTicketCode = this.value.ToString();
        return LeftPaddedTicketCode(currentTicketCode);
    }

    private string LeftPaddedTicketCode(string ticketCode)
    {
        int numberOfZeroesToPad = MaxTicketCodeLength - ticketCode.Length;
        
        if (numberOfZeroesToPad < 0)
        {
            throw new Exception("Ticket code overflow");
        }
        
        for(int counter = 0; counter < numberOfZeroesToPad; counter++)
        {
            ticketCode = "0" + ticketCode;
        }

        return ticketCode;
    }

    public Ticket? FindByCode(string ticketCode)
    {
        if (!Exists(ticketCode))
        {
            return null;
        }

        return this.ticketsByCode[ticketCode];
    }

    public void Store(Ticket ticket)
    {
		if (Exists(ticket.Code))
        {
			throw new Exception("Cannot store ticket. Code '" + ticket.Code + "' already exists.");
		}

		this.ticketsByCode.Add(ticket.Code, ticket);
    }

    public List<Ticket> FindByCarRateOrderByEndingDateTimeDesc(string carPlate, string rateName)
    {
        List<Ticket> ticketsOfCarAndRate = new List<Ticket>();

        if (this.ticketsByCode is null || this.ticketsByCode.Count == 0)
        {
            return ticketsOfCarAndRate;
        }

        foreach(Ticket ticket in this.ticketsByCode.Values)
        {
            if (ticket.CarPlate == carPlate && ticket.RateName == rateName)
            {
                ticketsOfCarAndRate.Add(ticket);
            }
        }

        ticketsOfCarAndRate.Sort(new TicketComparer());
        return ticketsOfCarAndRate;
    }

    public void Delete(string ticketCode)
    {
        if (!Exists(ticketCode))
        {
            throw new Exception("Cannot delete ticket. Code '" + ticketCode + "' does not exist.");
        }

        this.ticketsByCode.Remove(ticketCode);
    }

    public bool Exists(string ticketCode)
    {
		return this.ticketsByCode.ContainsKey(ticketCode);
	}
}