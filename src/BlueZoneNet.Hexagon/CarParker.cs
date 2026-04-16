using BlueZoneNet.Hexagon.Ports.Driven.ForObtainingRates;
using BlueZoneNet.Hexagon.Ports.Driven.ForPaying;
using BlueZoneNet.Hexagon.Ports.Driven.ForStoringTickets;
using BlueZoneNet.Hexagon.Ports.Driving.ForParkingCars;

namespace BlueZoneNet.Hexagon;

public class CarParker : IForParkingCars
{
    private readonly IForObtainingRates rateProvider;
    private readonly IForStoringTickets ticketStore;
    private readonly IForPaying paymentService;

    public CarParker(IForObtainingRates rateProvider, IForStoringTickets ticketStore, IForPaying eWalletService)
    {
        this.rateProvider = rateProvider;
        this.ticketStore = ticketStore;
        this.paymentService = eWalletService;
    }

    public Dictionary<string, Rate> GetAllRatesByName()
    {
        Dictionary<string, Rate> allRatesByName = new Dictionary<string, Rate>();
        List<Rate> allRates = this.rateProvider.FindAll();
        
        foreach(Rate rate in allRates)
        {
            allRatesByName.Add(rate.Name, rate);
        }

        return allRatesByName;
    }

    public string PurchaseTicket(PurchaseTicketRequest purchaseTicketRequest)
    {
        // Pay
        string ticketCode = this.ticketStore.NextCode();
        string paymentCard = purchaseTicketRequest.PaymentCard;
        double moneyToPay = purchaseTicketRequest.Amount;
        PayRequest payRequest = new PayRequest(ticketCode, paymentCard, moneyToPay);
        this.paymentService.Pay(payRequest);
        
        // Calc ending date-time
        string rateName = purchaseTicketRequest.RateName;
        Rate? rate = this.rateProvider.FindByName(rateName);
        RateCalculator rateCalculator = new RateCalculator(rate);
        DateTime clock = purchaseTicketRequest.Clock;
        DateTime starting = clock;
        DateTime ending = rateCalculator.GetUntilGivenAmount(starting, moneyToPay);
        
        // Store
        string carPlate = purchaseTicketRequest.CarPlate;
        Ticket ticket = new Ticket(ticketCode, carPlate, rateName, starting, ending, moneyToPay);
        this.ticketStore.Store(ticket);
        return ticketCode;
    }

    public Ticket? GetTicket(string ticketCode)
    {
        return this.ticketStore.FindByCode(ticketCode);
    }
}