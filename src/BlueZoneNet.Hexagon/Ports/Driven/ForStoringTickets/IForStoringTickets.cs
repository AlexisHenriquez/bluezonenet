namespace BlueZoneNet.Hexagon.Ports.Driven.ForStoringTickets;

/**
 * DRIVEN PORT
 */
public interface IForStoringTickets
{
    public string NextCode();
    public Ticket FindByCode (string ticketCode);
    public void Store (Ticket ticket);
    public List<Ticket> FindByCarRateOrderByEndingDateTimeDesc(string carPlate, string rateName);
    public void Delete (string ticketCode);
    public bool Exists (string ticketCode);
    public void SetNextCode (string ticketCode);
    public string NextAvailableCode();
}