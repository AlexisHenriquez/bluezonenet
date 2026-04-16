namespace BlueZoneNet.Hexagon.Ports.Driven.ForPaying;

/**
 * Exception thrown by the pay method in the "for paying" port,
 * when it has been any error and the payment didn't take place.
 */
[System.Serializable]
public class PayErrorException : System.Exception
{
    public PayErrorException() { }
    public PayErrorException(string message) : base(message) { }
    public PayErrorException(string message, System.Exception inner) : base(message, inner) { }
}