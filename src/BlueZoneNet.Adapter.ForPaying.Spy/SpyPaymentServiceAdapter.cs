using BlueZoneNet.Hexagon.Ports.Driven.ForPaying;

namespace BlueZoneNet.Adapter.ForPaying.Spy;

/**
 * Driven adapter that implements "forpaying" port with a "spy" test double.
 */
public class SpyPaymentServiceAdapter : IForPaying
{
    private List<PayRequest> paymentSpool;
    private int payErrorGenerationPercentage;

    public SpyPaymentServiceAdapter()
    {
        this.paymentSpool = new List<PayRequest>();
        SetPayErrorGenerationPercentage(0);
    }

    public void SetPayErrorGenerationPercentage(int percent)
    {
        this.payErrorGenerationPercentage = ValidatePercent(percent);
    }

    public PayRequest? LastPayRequest()
    {
        int spoolSize = this.paymentSpool.Count;
        
        if (spoolSize == 0)
        {
            return null;
        }
        
        return this.paymentSpool[spoolSize - 1];
    }

    public void Pay(PayRequest payRequest)
    {
        this.paymentSpool.Add(payRequest);
        ThrowPayErrorExceptionRandomly();
    }

    private void ThrowPayErrorExceptionRandomly()
    {
        int numberBetween1And100 = new Random().Next(100) + 1;
        if (numberBetween1And100 <= this.payErrorGenerationPercentage)
        {
            throw new PayErrorException("Payment failed");
        }
    }

    private int ValidatePercent(int percent)
    {
        if (percent < 0)
        {
            return  0;
        }

        if (percent > 100)
        {
            return  100;
        }

        return percent;
    }
}