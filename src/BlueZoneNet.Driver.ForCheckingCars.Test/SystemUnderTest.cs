using BlueZoneNet.Hexagon.Ports.Driving.ForCheckingCars;
using BlueZoneNet.Hexagon.Ports.Driving.ForConfiguringApp;

namespace BlueZoneNet.Driver.ForCheckingCars.Test;

public class SystemUnderTest
{
    private static readonly SystemUnderTest instance;

    static SystemUnderTest()
    {
        instance = new SystemUnderTest();
    }

    private SystemUnderTest() { }

    public static SystemUnderTest Instance => instance;

    public IForCheckingCars CarChecker { get; set; } = default!;

    public IForConfiguringApp AppConfigurator { get; set; } = default!;
}