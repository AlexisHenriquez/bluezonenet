namespace BlueZoneNet.Hexagon.Ports.Driven.ForStoringTickets;

/**
 * DTO with the data of a parking ticket:
 * 		code				Unique identifier of the ticket
 * 		carPlate			Plate of the car that has been parked
 * 		rateName			Rate name of the zone where the car is parked at
 * 		startingDateTime	When the parking period begins
 * 		endingDateTime		When the parking period expires
 * 		price				Amount of money paid for the ticket
 * 		paymentId			Id of the ticket purchasing transaction in the payment service
 */
public class Ticket
{
	public string Code { get; set; } = default!;
	public string CarPlate { get; set; } = default!;
	public string RateName { get; set; } = default!;
	public DateTime StartingDateTime { get; set; }
	public DateTime EndingDateTime { get; set; }
	public double Price { get; set; }
}