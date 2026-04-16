using BlueZoneNet.Hexagon.Factory;
using BlueZoneNet.Hexagon.Ports.Driven.ForObtainingRates;
using BlueZoneNet.Hexagon.Ports.Driven.ForPaying;
using BlueZoneNet.Hexagon.Ports.Driven.ForStoringTickets;
using BlueZoneNet.Hexagon.Ports.Driving.ForCheckingCars;
using BlueZoneNet.Hexagon.Ports.Driving.ForConfiguringApp;
using BlueZoneNet.Hexagon.Ports.Driving.ForParkingCars;

namespace BlueZoneNet.Hexagon;

/**
 * Application
 * Offers driver ports as API.
 * Has a configurable dependency on driven ports as RI (required interface).
 */
public class AppFromDrivenSide : IBlueZoneApp
{
    // Driver ports
    private IForParkingCars? carParker;
    private IForCheckingCars? carChecker;
    private IForConfiguringApp? appConfigurator;
    // Driven ports
    private readonly IForObtainingRates rateProvider;
    private readonly IForStoringTickets ticketStore;
    private readonly IForPaying paymentService;

    public AppFromDrivenSide(IForObtainingRates rateProvider, IForStoringTickets ticketStore, IForPaying paymentService)
    {
        this.rateProvider = rateProvider;
        this.ticketStore = ticketStore;
        this.paymentService = paymentService;
    }

    public IForParkingCars CarParker()
    {
        if (this.carParker is null)
        {
            this.carParker = new CarParker(this.rateProvider, this.ticketStore, this.paymentService);
        }

        return this.carParker;
    }

    public IForCheckingCars CarChecker()
    {
        if (this.carChecker is null)
        {
            this.carChecker = new CarChecker(this.ticketStore);
        }

        return this.carChecker;
    }

    public IForConfiguringApp AppConfigurator()
    {
        if (this.appConfigurator is null)
        {
            this.appConfigurator = new AppConfigurator(this.rateProvider,this.ticketStore,this.paymentService);
        }

        return this.appConfigurator;
    }
}