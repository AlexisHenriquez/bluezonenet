namespace BlueZoneNet.Hexagon.Ports.Driven.ForPaying;

/**
 * DRIVEN PORT
 */
public interface IForPaying
{
    public void Pay(PayRequest payRequest);
    public PayRequest? LastPayRequest();
    public void SetPayErrorGenerationPercentage(int percent);
}