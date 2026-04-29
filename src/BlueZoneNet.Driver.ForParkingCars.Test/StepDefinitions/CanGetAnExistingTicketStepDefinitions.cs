using System;
using BlueZoneNet.Hexagon.Ports.Driven.ForStoringTickets;

namespace BlueZoneNet.Driver.ForParkingCars.Test.StepDefinitions;

[Binding]
public class CanGetAnExistingTicketStepDefinitions
{
    private readonly ScenarioContext scenarioContext;

    public CanGetAnExistingTicketStepDefinitions(ScenarioContext scenarioContext)
    {
        this.scenarioContext = scenarioContext;
    }

    [Given("there is the following ticket at ticket repository:")]
    public void GivenThereIsTheFollowingTicketAtTicketRepository(DataTable dataTable)
    {
        Ticket ticket = dataTable.ToTicketsList()[0];
        ForParkingCarsTestDriver.Instance.AppConfigurator.CreateTicket(ticket);
    }

    [When("I ask for getting the ticket with code {string}")]
    public void WhenIAskForGettingTheTicketWithCode(string ticketCode)
    {
        Ticket? ticket = ForParkingCarsTestDriver.Instance.CarParker.GetTicket(ticketCode);
        if (ticket is not null)
        {
            scenarioContext.Add("CurrentTicketWithCode", ticket);
        }
    }

    [Then("I should obtain the following ticket:")]
    public void ThenIShouldObtainTheFollowingTicket(DataTable dataTable)
    {
        Ticket expectedTicketWithCode = dataTable.ToTicketsList()[0];
        Assert.That(scenarioContext["CurrentTicketWithCode"] as Ticket, Is.EqualTo(expectedTicketWithCode).Using(new TicketComparer()));
    }
}
