using BlueZoneNet.Hexagon.Ports.Driven.ForObtainingRates;
using BlueZoneNet.Hexagon.Ports.Driven.ForStoringTickets;

namespace BlueZoneNet.Hexagon.Ports.Driving.ForParkingCars;

/**
 * DRIVER PORT
 */
public interface ForParkingCars
{
	/**
	 * Returns the available rates in the city, indexed by name.
	 *
	 * return a map of Rate objects, with the rate name as the key.
	 * see Rate
	 */
	public Dictionary<string, Rate> GetAllRatesByName();

	/**
	 * It pays for a parking ticket, which will be valid for the following period of time:
	 *		- Starting:	Current date-time.
	 *		- Ending:	Date-time calculated from the payment amount, according to the rate of the zone the car is parked at.
	 * The payment is done by charging the amount to the card given in the purchase ticket request.
	 *
	 * purchaseTicketRequest Data needed for purchasing a parking ticket.
	 * returns The code of the purchased ticket, useful for retrieving the whole ticket data later on.
	 * throws PayErrorException When any error occur paying the amount with the card.
	 * see PurchaseTicketRequest
	 * see Ticket
	 * see PayErrorException
	 */
	public string PurchaseTicket(PurchaseTicketRequest purchaseTicketRequest);

	/**
	 * Given the code of a previously purchased ticket, returns the whole data of the ticket.
	 *
	 * ticketCode Code of a purchased ticket.
	 * returns The ticket with the given ticket code, or null if it doesn't exist any ticket with such a code.
	 */
	public Ticket GetTicket(string ticketCode);
}