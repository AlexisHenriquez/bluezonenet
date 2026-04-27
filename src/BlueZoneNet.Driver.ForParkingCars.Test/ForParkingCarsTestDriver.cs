using BlueZoneNet.Hexagon.Ports.Driving.ForConfiguringApp;
using BlueZoneNet.Hexagon.Ports.Driving.ForParkingCars;

namespace BlueZoneNet.Driver.ForParkingCars.Test;

/**
 * 
 * Driver that runs "forparkingcars" port test cases.
 * 
 * It uses Cucumber testing automation tool
 * 
 * Test cases are scenarios described in feature files using Gherkin language.
 * 
 * There will be a feature file for each use case, containing scenarios as acceptance tests for the use case.
 * 
 * The feature is written using the "role-goal-benefit" template
 * ( "AS a role I WANT some goal SO THAT some benefit" ).
 * 
 * Scenarios are written using the "given-when-then" template
 * ( "GIVEN some context WHEN some action is carried out THEN a set of consequences should be obtained" ).
 * 
 */
public class ForParkingCarsTestDriver
{
    private static readonly ForParkingCarsTestDriver instance;

    static ForParkingCarsTestDriver()
    {
        instance = new ForParkingCarsTestDriver();
    }

    private ForParkingCarsTestDriver() { }

    public static ForParkingCarsTestDriver Instance => instance;

    public IForParkingCars CarParker { get; set; } = default!;

    public IForConfiguringApp AppConfigurator { get; set; } = default!;
}