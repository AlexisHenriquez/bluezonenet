namespace BlueZoneNet.Hexagon.Ports.Driven.ForObtainingRates;

/**
 * DTO with data of a rate:
 *		name			Rate name. Unique. Two uppercase words separated by _
 * 		amountPerHour	Cost in euros of parking the car during one hour
 */
public class Rate
{
	public string Name { get; set; } = default!;
	public double AmountPerHour { get; set; }

	public override string ToString()
    {
		return this.Name + " ( " + this.AmountPerHour + "€ / hour )";
	}
}