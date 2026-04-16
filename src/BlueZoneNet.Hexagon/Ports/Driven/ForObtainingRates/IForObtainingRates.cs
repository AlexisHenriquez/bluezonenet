namespace BlueZoneNet.Hexagon.Ports.Driven.ForObtainingRates;

/**
 * DRIVEN PORT
 */
public interface IForObtainingRates
{
    public List<Rate> FindAll();
    public Rate FindByName(string rateName);
    public void AddRate (Rate rate);
    public bool Exists (string rateName);
    public void Empty();
}