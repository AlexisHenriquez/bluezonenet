using BlueZoneNet.Adapter.ForObtainingRates.Stub;
using BlueZoneNet.Adapter.ForPaying.Spy;
using BlueZoneNet.Adapter.ForStoringTickets.Fake;
using BlueZoneNet.Hexagon;
using BlueZoneNet.Hexagon.Ports.Driven.ForStoringTickets;
using NUnit.Framework;

namespace BlueZoneNet.Driver.ForCheckingCars.Test.IllegallyParkedCar;

/*
SCENARIO: At least one valid ticket
-----------------------------------

    GIVEN
        this ticket at repository:
            | code       | carPlate | rateName   | startingDateTime | endingDateTime   | price |
            | 0000001155 | 1365MDS  | GREEN_ZONE | 2022/06/17 12:15 | 2022/06/17 12:45 | 0.60  |
    WHEN
        I check at "2022/06/17 12:30" car "1365MDS" parked in an area with rate "GREEN_ZONE"
    THEN
        illegally parked car should be "false"
*/
[TestFixture]
public class AtLeastOneValidTicket
{
    private Ticket ticket;
    private DateTime currentDateTime;
    private string carPlate;
    private string rateName;
    private bool expectedIllegallyParkedCar;

    // SystemUnderTest Outcome
    private bool illegallyParkedCar;

    [OneTimeSetUp]
    public void SetUp()
    {
        ticket = new Ticket("0000001155","1365MDS","GREEN_ZONE",new DateTime(2022, 6, 17, 12, 15, 0),new DateTime(2022, 6, 17, 12, 45, 0),0.60);
        long minutesOfTicket = (ticket.EndingDateTime - ticket.StartingDateTime).Minutes;
        currentDateTime = ticket.StartingDateTime.AddMinutes(minutesOfTicket / 2);
        carPlate = ticket.CarPlate;
        rateName = ticket.RateName;
        expectedIllegallyParkedCar = false;

        IForStoringTickets forStoringTickets = new FakeTicketStoreAdapter();
        SystemUnderTest.Instance.AppConfigurator = new AppConfigurator(new StubRateProviderAdapter(), forStoringTickets, new SpyPaymentServiceAdapter());
        SystemUnderTest.Instance.CarChecker = new CarChecker(forStoringTickets);
    }

    [Test]
    public void CarShouldNotBeIllegallyParkedWhenAtLeastOneValidTicket()
    {
        GivenThisTicketAtRepository(ticket);
        WhenICheckAtCurrentDateTimeCarParkedInAreaWithRate(currentDateTime, carPlate, rateName);
        ThenIllegallyParkedCarShouldBe(expectedIllegallyParkedCar);
    }

    private void GivenThisTicketAtRepository(Ticket ticket)
    {
        TestContext.Out.WriteLine("GIVEN this ticket at repository:" + ticket);
        SystemUnderTest.Instance.AppConfigurator.CreateTicket(ticket);
    }

    private void WhenICheckAtCurrentDateTimeCarParkedInAreaWithRate(DateTime currentDateTime, string carPlate, string rateName)
    {
        TestContext.Out.WriteLine("WHEN I check at '" + currentDateTime + "' car '" + carPlate + "' parked in an area with rate '" + rateName + "'");
        this.illegallyParkedCar = SystemUnderTest.Instance.CarChecker.IllegallyParkedCar(currentDateTime, carPlate, rateName);
    }

    private void ThenIllegallyParkedCarShouldBe(bool expectedIllegallyParkedCar)
    {
        TestContext.Out.WriteLine("THEN illegally parked car ('" + this.illegallyParkedCar + "') should be '" + expectedIllegallyParkedCar + "'");
        Assert.That(this.illegallyParkedCar, Is.EqualTo(expectedIllegallyParkedCar));
    }
}