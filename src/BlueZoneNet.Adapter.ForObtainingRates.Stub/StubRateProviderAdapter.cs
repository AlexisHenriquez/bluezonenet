using BlueZoneNet.Hexagon.Ports.Driven.ForObtainingRates;

namespace BlueZoneNet.Adapter.ForObtainingRates.Stub;

/**
 * Driven adapter that implements "forobtainingrates" port with a stub test double.
 */
public class StubRateProviderAdapter : IForObtainingRates
{
	private List<Rate> rates;

	public StubRateProviderAdapter()
    {
		this.rates = new List<Rate>();
	}

	public List<Rate> FindAll()
    {
		return this.rates;
	}

	public Rate? FindByName(string rateName)
    {
		int occurrences = 0;
		Rate? rateFound = null;

		foreach(Rate rate in this.rates)
        {
			if (rate.Name == rateName)
            {
				rateFound = rate;
				occurrences++;
			}
		}

		if (occurrences == 0)
        {
			return null;
		}

		if (occurrences > 1)
        {
			throw new Exception("Multiple rates found with name = " + rateName);
		}

		return rateFound;
	}

	public void AddRate(Rate rate)
    {
		if (Exists(rate.Name))
        {
			throw new Exception("Cannot add rate to repository. Rate name '" + rate.Name + "' already exists.");
		}

		this.rates.Add(rate);
	}

	public bool Exists(string rateName)
    {
		return (FindByName(rateName) is not null);
	}

	public void Empty()
    {
		this.rates.Clear();
	}
}