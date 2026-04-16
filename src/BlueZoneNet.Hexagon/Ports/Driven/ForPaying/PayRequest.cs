namespace BlueZoneNet.Hexagon.Ports.Driven.ForPaying;

public class PayRequest
{
    public string TicketCode { get; set; } = default!;
    public string PaymentCard { get; set; } = default!;
    public double Amount { get; set; }
}