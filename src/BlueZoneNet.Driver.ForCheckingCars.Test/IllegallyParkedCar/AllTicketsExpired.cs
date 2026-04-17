using BlueZoneNet.Adapter.ForObtainingRates.Stub;
using BlueZoneNet.Adapter.ForPaying.Spy;
using BlueZoneNet.Adapter.ForStoringTickets.Fake;
using BlueZoneNet.Hexagon;
using BlueZoneNet.Hexagon.Ports.Driven.ForStoringTickets;
using NUnit.Framework;

namespace BlueZoneNet.Driver.ForCheckingCars.Test.IllegallyParkedCar;

/*
SCENARIO: All tickets expired
-----------------------------

    GIVEN
        these tickets at repository:
            | code       | carPlate | rateName   | startingDateTime | endingDateTime   | price |
            | 0000000658 | 6989JJH  | BLUE_ZONE  | 2022/06/11 08:30 | 2022/06/11 09:00 | 0.40  |
            | 0000000659 | 6989JJH  | BLUE_ZONE  | 2022/06/11 09:15 | 2022/06/11 10:15 | 0.80  |
            | 0000000660 | 1365MDS  | BLUE_ZONE  | 2022/06/11 09:30 | 2022/06/11 11:00 | 1.20  |
            | 0000000661 | 6989JJH  | GREEN_ZONE | 2022/06/11 10:00 | 2022/06/11 10:30 | 0.60  |
    WHEN
        I check at "2022/06/11 10:16" car "6989JJH" parked in an area with rate "BLUE_ZONE"
    THEN
        illegally parked car should be "true"
*/
[TestFixture]
public class AllTicketsExpired
{
    private List<Ticket> tickets;
    private DateTime currentDateTime;
    private string carPlate;
    private string rateName;
    private bool expectedIllegallyParkedCar;

    // SystemUnderTest Outcome
	private bool illegallyParkedCar;

    [OneTimeSetUp]
    public void SetUp()
    {
        Ticket ticket1 = new Ticket("0000000658","6989JJH","BLUE_ZONE",new DateTime(2022, 6, 11, 8, 30, 0),new DateTime(2022, 6, 11, 9, 0, 0),0.40);
		Ticket ticket2 = new Ticket("0000000659","6989JJH","BLUE_ZONE",new DateTime(2022, 6, 11, 9, 15, 0),new DateTime(2022, 6, 11, 10, 15, 0),0.80);
		Ticket ticket3 = new Ticket("0000000660","1365MDS","BLUE_ZONE",new DateTime(2022, 6, 11, 9, 30, 0),new DateTime(2022, 6, 11, 11, 0, 0),1.20);
		Ticket ticket4 = new Ticket("0000000661","6989JJH","GREEN_ZONE",new DateTime(2022, 6, 11, 10, 0, 0),new DateTime(2022, 6, 11, 10, 30, 0),0.60);
		tickets = [ticket1, ticket2, ticket3, ticket4];

        currentDateTime = ticket2.EndingDateTime.AddMinutes(1);
        carPlate = ticket2.CarPlate;
        rateName = ticket2.RateName;
        expectedIllegallyParkedCar = true;

        IForStoringTickets forStoringTickets = new FakeTicketStoreAdapter();
        SystemUnderTest.Instance.AppConfigurator = new AppConfigurator(new StubRateProviderAdapter(), forStoringTickets, new SpyPaymentServiceAdapter());
        SystemUnderTest.Instance.CarChecker = new CarChecker(forStoringTickets);
    }

    [Test]
    public void CarShouldBeIllegallyParkedWhenAllTicketsExpired()
    {
		GivenTheseTicketsAtRepository(tickets);
		WhenICheckAtCurrentDateTimeCarParkedInAreaWithRate(currentDateTime, carPlate, rateName);
		ThenIllegallyParkedCarShouldBe(expectedIllegallyParkedCar);
	}

    private void GivenTheseTicketsAtRepository(List<Ticket> tickets)
    {
		TestContext.Out.WriteLine("GIVEN these tickets at repository: " + tickets);
		foreach(Ticket ticket in tickets)
        {
			SystemUnderTest.Instance.AppConfigurator.CreateTicket(ticket);
		}
	}

    private void WhenICheckAtCurrentDateTimeCarParkedInAreaWithRate(DateTime currentDateTime, string carPlate, string rateName)
    {
		TestContext.Out.WriteLine("WHEN I check at '" + currentDateTime + "' car '" + carPlate + "' parked in an area with rate '" + rateName + "'");
		this.illegallyParkedCar = SystemUnderTest.Instance.CarChecker.IllegallyParkedCar(currentDateTime, carPlate, rateName);
	}

    private void ThenIllegallyParkedCarShouldBe(bool expectedIllegallyParkedCar)
    {
		TestContext.Out.WriteLine("THEN illegally parked car ('" + this.illegallyParkedCar + "') should be '" + expectedIllegallyParkedCar + "'" );
		Assert.That(this.illegallyParkedCar, Is.EqualTo(expectedIllegallyParkedCar));
	}
}