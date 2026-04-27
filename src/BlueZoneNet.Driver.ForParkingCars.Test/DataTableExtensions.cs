using BlueZoneNet.Hexagon.Ports.Driven.ForObtainingRates;

namespace BlueZoneNet.Driver.ForParkingCars.Test;

public static class DataTableExtensions
{
    public static Dictionary<string, Rate> ToRatesDictionary(this DataTable dataTable)
    {
        return dataTable.Rows.ToDictionary(
            row => row["key"],
            row => new Rate() { AmountPerHour = double.Parse(row["amountPerHour"]), Name = row["name"] }
        );
    }

    public static List<Rate> ToRatesList(this DataTable dataTable)
    {
        return dataTable.CreateSet<Rate>().ToList();
    }
}