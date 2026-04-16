using BlueZoneNet.Hexagon.Ports.Driven.ForObtainingRates;
using BlueZoneNet.Hexagon.Ports.Driven.ForPaying;
using BlueZoneNet.Hexagon.Ports.Driven.ForStoringTickets;
using BlueZoneNet.Hexagon.Ports.Driving.ForCheckingCars;
using BlueZoneNet.Hexagon.Ports.Driving.ForConfiguringApp;
using BlueZoneNet.Hexagon.Ports.Driving.ForParkingCars;

namespace BlueZoneNet.Hexagon.Factory;

/**
 * API
 * Driver ports
 */
public interface IBlueZoneApp
{
    public static IBlueZoneApp GetInstance(IForObtainingRates rateProvider, IForStoringTickets ticketStore, IForPaying paymentService)
    {
        return new AppFromDrivenSide(rateProvider, ticketStore, paymentService);
    }

    public IForParkingCars CarParker();
    public IForCheckingCars CarChecker();
    public IForConfiguringApp AppConfigurator();
}