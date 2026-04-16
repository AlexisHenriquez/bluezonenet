namespace BlueZoneNet.Hexagon.Ports.Driving.ForParkingCars;

/**
 * DTO with the data needed for purchasing a parking ticket:
 * 		carPlate			Plate of the car to be parked
 * 		rateName			Rate name of the zone where the car will be parked at
 *		clock				A clock to get current date-time from, since it will be the starting date-time of the ticket period
 * 		amount				Money (euros) to be paid for the parking ticket
 * 		paymentCard			Number of the card where the amount will be charged
 */
public class PurchaseTicketRequest
{
	public string CarPlate { get; set; } = default!;
	public string RateName { get; set; } = default!;
	public DateTime Clock { get; set; }
	public double Amount { get; set; }
	public string PaymentCard { get; set; } = default!;
}