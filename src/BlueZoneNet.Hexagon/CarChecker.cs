using BlueZoneNet.Hexagon.Ports.Driven.ForStoringTickets;
using BlueZoneNet.Hexagon.Ports.Driving.ForCheckingCars;

namespace BlueZoneNet.Hexagon;

public class CarChecker : IForCheckingCars
{
    private readonly IForStoringTickets ticketStore;

    public CarChecker(IForStoringTickets ticketStore)
    {
        this.ticketStore = ticketStore;
    }

    public bool IllegallyParkedCar(DateTime clock, string carPlate, string rateName)
    {
        List<Ticket> ticketsOfCarAndRate = this.ticketStore.FindByCarRateOrderByEndingDateTimeDesc(carPlate, rateName);
        
        if (ticketsOfCarAndRate is null || ticketsOfCarAndRate.Count == 0)
        {
            return true;
        }

        DateTime currentDateTime = clock;
        DateTime latestEndingDateTime = ticketsOfCarAndRate[0].EndingDateTime;
        
        return currentDateTime > latestEndingDateTime;
    }
}