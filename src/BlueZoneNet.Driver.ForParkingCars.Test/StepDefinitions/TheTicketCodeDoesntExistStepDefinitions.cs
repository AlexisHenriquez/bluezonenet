using BlueZoneNet.Hexagon.Ports.Driven.ForStoringTickets;

namespace BlueZoneNet.Driver.ForParkingCars.Test.StepDefinitions;

[Binding]
public class TheTicketCodeDoesntExistStepDefinitions
{
    private readonly ScenarioContext scenarioContext;

    public TheTicketCodeDoesntExistStepDefinitions(ScenarioContext scenarioContext)
    {
        this.scenarioContext = scenarioContext;
    }

    [Then("I should obtain no ticket")]
    public void ThenIShouldObtainNoTicket()
    {
        Assert.That(scenarioContext.ContainsKey("CurrentTicketWithCode"), Is.False);
    }
}