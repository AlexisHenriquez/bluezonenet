namespace BlueZoneNet.Hexagon.Ports.Driven.ForPaying;

public class PayRequest
{
    public string TicketCode { get; set; } = default!;
    public string PaymentCard { get; set; } = default!;
    public double Amount { get; set; }

    public PayRequest(string ticketCode, string paymentCard, double moneyToPay)
    {
        TicketCode = ticketCode;
        PaymentCard = paymentCard;
        Amount = moneyToPay;
    }
}