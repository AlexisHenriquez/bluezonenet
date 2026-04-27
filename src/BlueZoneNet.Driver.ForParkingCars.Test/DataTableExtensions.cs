using BlueZoneNet.Hexagon.Ports.Driven.ForObtainingRates;

namespace BlueZoneNet.Driver.ForParkingCars.Test;

public static class DataTableExtensions
{
    public static Dictionary<string, Rate> ToRatesDictionary(this DataTable dataTable)
    {
        Dictionary<string, Rate> rates = new Dictionary<string, Rate>();

        foreach (var row in dataTable.Rows)
        {
            rates.Add(row["key"], new Rate() { AmountPerHour = double.Parse(row["amountPerHour"]), Name = row["name"] });
        }

        return rates;
    }
}