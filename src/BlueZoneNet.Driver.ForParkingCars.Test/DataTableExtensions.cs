using BlueZoneNet.Hexagon.Ports.Driven.ForObtainingRates;
using BlueZoneNet.Hexagon.Ports.Driven.ForPaying;
using BlueZoneNet.Hexagon.Ports.Driven.ForStoringTickets;
using BlueZoneNet.Hexagon.Ports.Driving.ForParkingCars;

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

    public static List<Ticket> ToTicketsList(this DataTable dataTable)
    {
        return dataTable.CreateSet<Ticket>().ToList();
    }

    public static List<PurchaseTicketRequest> ToPurchaseTicketRequestsList(this DataTable dataTable)
    {
        return dataTable.CreateSet<PurchaseTicketRequest>().ToList();
    }

    public static List<PayRequest> ToPayRequestsList(this DataTable dataTable)
    {
        return dataTable.CreateSet<PayRequest>().ToList();
    }
}