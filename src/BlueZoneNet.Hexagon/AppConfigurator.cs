using BlueZoneNet.Hexagon.Ports.Driven.ForObtainingRates;
using BlueZoneNet.Hexagon.Ports.Driven.ForPaying;
using BlueZoneNet.Hexagon.Ports.Driven.ForStoringTickets;
using BlueZoneNet.Hexagon.Ports.Driving.ForConfiguringApp;

namespace BlueZoneNet.Hexagon;

public class AppConfigurator : IForConfiguringApp
{
    private readonly IForObtainingRates rateProvider;
    private readonly IForStoringTickets ticketStore;
    private readonly IForPaying paymentService;

    public AppConfigurator(IForObtainingRates rateProvider, IForStoringTickets ticketStore, IForPaying paymentService)
    {
        this.rateProvider = rateProvider;
        this.ticketStore = ticketStore;
        this.paymentService = paymentService;
    }

    public void InitRateProviderWith(List<Rate> rates)
    {
        this.rateProvider.Empty();
        
        foreach(Rate rate in rates)
        {
            if (!this.rateProvider.Exists(rate.Name))
            {
                this.rateProvider.AddRate(rate);
            }
        }
    }

    public void CreateTicket(Ticket ticket)
    {
        if (!this.ticketStore.Exists(ticket.Code))
        {
            this.ticketStore.Store(ticket);
        }
    }

    public void EraseTicket(string ticketCode)
    {
        if (this.ticketStore.Exists(ticketCode))
        {
            this.ticketStore.Delete(ticketCode);
        }
    }

    public void SetNextTicketCodeToReturn(string ticketCode)
    {
        this.ticketStore.SetNextCode(ticketCode);
    }

    public string GetNextTicketCodeToReturn()
    {
        return this.ticketStore.NextAvailableCode();
    }

    public PayRequest? GetLastPayRequestDone()
    {
        return this.paymentService.LastPayRequest();
    }

    public void SetPaymentErrorPercentage(int percent)
    {
        this.paymentService.SetPayErrorGenerationPercentage(percent);
    }
}