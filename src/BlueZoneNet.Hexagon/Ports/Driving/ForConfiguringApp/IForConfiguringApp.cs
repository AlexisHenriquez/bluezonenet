using BlueZoneNet.Hexagon.Ports.Driven.ForObtainingRates;
using BlueZoneNet.Hexagon.Ports.Driven.ForPaying;
using BlueZoneNet.Hexagon.Ports.Driven.ForStoringTickets;

namespace BlueZoneNet.Hexagon.Ports.Driving.ForConfiguringApp;

/**
 * DRIVER PORT
 */
public interface IForConfiguringApp
{
    public void InitRateProviderWith(List<Rate> rates);
    public void CreateTicket(Ticket ticket);
    public void EraseTicket(string ticketCode);
    public void SetNextTicketCodeToReturn(string ticketCode);
    public string GetNextTicketCodeToReturn();
    public PayRequest GetLastPayRequestDone();
    public void SetPaymentErrorPercentage(int percent);
}