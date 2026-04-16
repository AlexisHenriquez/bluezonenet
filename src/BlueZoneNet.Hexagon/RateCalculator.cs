using BlueZoneNet.Hexagon.Ports.Driven.ForObtainingRates;

namespace BlueZoneNet.Hexagon;

public class RateCalculator
{
    private readonly Rate rate;

    public RateCalculator(Rate rate)
    {
        this.rate = rate;
    }

    public DateTime GetUntilGivenAmount(DateTime from, double amount)
    {
        // minutes = (amount*60)/amountPerHour
        int minutes = (int) ((amount*60.0) / this.rate.AmountPerHour);
        return from.AddMinutes(minutes);
    }
}