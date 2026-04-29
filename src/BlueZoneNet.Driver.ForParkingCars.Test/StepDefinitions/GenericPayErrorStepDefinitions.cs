using BlueZoneNet.Hexagon.Ports.Driven.ForPaying;
using BlueZoneNet.Hexagon.Ports.Driven.ForStoringTickets;
using BlueZoneNet.Hexagon.Ports.Driving.ForParkingCars;

namespace BlueZoneNet.Driver.ForParkingCars.Test.StepDefinitions;

[Binding]
public class GenericPayErrorStepDefinitions
{
    private readonly ScenarioContext scenarioContext;

    public GenericPayErrorStepDefinitions(ScenarioContext scenarioContext)
    {
        this.scenarioContext = scenarioContext;
    }

    [Given("next available ticket code is {string}")]
    public void GivenNextAvailableTicketCodeIs(string ticketCode)
    {
        ForParkingCarsTestDriver.Instance.AppConfigurator.SetNextTicketCodeToReturn(ticketCode);
    }

    [Given("there is no ticket with code {string} at ticket repository")]
    public void GivenThereIsNoTicketWithCodeAtTicketRepository(string ticketCode)
    {
        ForParkingCarsTestDriver.Instance.AppConfigurator.EraseTicket(ticketCode);
    }

    [Given("the payment is not valid")]
    public void GivenThePaymentIsNotValid()
    {
        ForParkingCarsTestDriver.Instance.AppConfigurator.SetPaymentErrorPercentage(100);
    }

    [When("I do the following purchase ticket request:")]
    public void WhenIDoTheFollowingPurchaseTicketRequest(DataTable dataTable)
    {
        try
        {
            PurchaseTicketRequest purchaseTicketRequest = dataTable.ToPurchaseTicketRequestsList()[0];
            string ticketCode = ForParkingCarsTestDriver.Instance.CarParker.PurchaseTicket(purchaseTicketRequest);
            scenarioContext.Add("PurchasedTicketCode", ticketCode);
        }
        catch(PayErrorException payErrorException)
        {
            scenarioContext.Add("PayErrorException", payErrorException);
        }
    }

    [Then("I should obtain no ticket code")]
    public void ThenIShouldObtainNoTicketCode()
    {
        Assert.That(scenarioContext.ContainsKey("PurchasedTicketCode"), Is.False);
    }

    [Then("there should be no ticket with code {string} at ticket repository")]
    public void ThenThereShouldBeNoTicketWithCodeAtTicketRepository(string ticketCode)
    {
        Ticket? ticketAtRepo = ForParkingCarsTestDriver.Instance.CarParker.GetTicket(ticketCode);
        Assert.That(ticketAtRepo, Is.Null);
    }

    [Then("next available ticket code should be {string}")]
    public void ThenNextAvailableTicketCodeShouldBe(string expectedNextTicketCode)
    {
        string currentNextTicketCode = ForParkingCarsTestDriver.Instance.AppConfigurator.GetNextTicketCodeToReturn();
        Assert.That(currentNextTicketCode, Is.EqualTo(expectedNextTicketCode));
    }

    [Then("the following pay request should have been done to payment service:")]
    public void ThenTheFollowingPayRequestShouldHaveBeenDoneToPaymentService(DataTable dataTable)
    {
        PayRequest expectedPayRequest = dataTable.ToPayRequestsList()[0];
        PayRequest? lastPayRequest = ForParkingCarsTestDriver.Instance.AppConfigurator.GetLastPayRequestDone();
        Assert.That(lastPayRequest, Is.EqualTo(expectedPayRequest).Using(new PayRequestComparer()));
    }

    [Then("a PayErrorException should have been thrown")]
    public void ThenAPayErrorExceptionShouldHaveBeenThrown()
    {
        Assert.That(scenarioContext["PayErrorException"], Is.Not.Null);
    }
}