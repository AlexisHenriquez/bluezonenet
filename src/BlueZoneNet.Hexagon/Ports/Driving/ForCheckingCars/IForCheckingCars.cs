namespace BlueZoneNet.Hexagon.Ports.Driving.ForCheckingCars;

/**
 * DRIVER PORT
 */
public interface IForCheckingCars
{
	/**
	 * A car is illegally parked at a zone, if the car does not have any valid ticket for the zone rate at the current date-time.
	 * A ticket is valid if the given date-time is between the starting and ending date-time of the ticket.
	 *
	 * clock		Clock to get current date-time from
	 * carPlate 	Plate of the car that we want to check
	 * rateName	    Rate name of the zone where the car to check is parked at
	 * returns      "true" if the car has no valid ticket for the rate at current date-time,
	 * 		        "false" otherwise.
	 */
	public bool IllegallyParkedCar(DateTime clock, string carPlate, string rateName);
}