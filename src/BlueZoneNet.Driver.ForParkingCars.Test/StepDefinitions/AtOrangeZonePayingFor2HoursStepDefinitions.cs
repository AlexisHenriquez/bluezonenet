using BlueZoneNet.Hexagon.Ports.Driven.ForStoringTickets;

namespace BlueZoneNet.Driver.ForParkingCars.Test.StepDefinitions;

[Binding]
public class AtOrangeZonePayingFor2HoursStepDefinitions
{
    private readonly ScenarioContext scenarioContext;

    public AtOrangeZonePayingFor2HoursStepDefinitions(ScenarioContext scenarioContext)
    {
        this.scenarioContext = scenarioContext;
    }

    [Given("the payment is valid")]
    public void GivenThePaymentIsValid()
    {
        ForParkingCarsTestDriver.Instance.AppConfigurator.SetPaymentErrorPercentage(0);
    }

    [Then("I should obtain the ticket code {string}")]
    public void ThenIShouldObtainTheTicketCode(string expectedPurchasedTicketCode)
    {
        Assert.That(scenarioContext["PurchasedTicketCode"], Is.EqualTo(expectedPurchasedTicketCode));
    }

    [Then("there should be the following ticket at ticket repository:")]
    public void ThenThereShouldBeTheFollowingTicketAtTicketRepository(DataTable dataTable)
    {
        Ticket expectedPurchasedTicket = dataTable.ToTicketsList()[0];
        Ticket? ticketAtRepo = ForParkingCarsTestDriver.Instance.CarParker.GetTicket(expectedPurchasedTicket.Code);
        Assert.That(ticketAtRepo, Is.EqualTo(expectedPurchasedTicket).Using(new TicketComparer()));
    }

    [Then("no PayErrorException should have been thrown")]
    public void ThenNoPayErrorExceptionShouldHaveBeenThrown()
    {
        Assert.That(scenarioContext.ContainsKey("PayErrorException"), Is.False);
    }
}