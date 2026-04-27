using BlueZoneNet.Adapter.ForObtainingRates.Stub;
using BlueZoneNet.Adapter.ForPaying.Spy;
using BlueZoneNet.Adapter.ForStoringTickets.Fake;
using BlueZoneNet.Hexagon;
using BlueZoneNet.Hexagon.Ports.Driven.ForObtainingRates;
using BlueZoneNet.Hexagon.Ports.Driven.ForPaying;
using BlueZoneNet.Hexagon.Ports.Driven.ForStoringTickets;

namespace BlueZoneNet.Driver.ForParkingCars.Test.StepDefinitions;

[Binding]
public class GetAllRatesByNameStepDefinitions
{
    private Dictionary<string, Rate>? ratesByName;

    [Given("there are the following rates at rate repository:")]
    public void GivenThereAreTheFollowingRatesAtRateRepository(DataTable dataTable)
    {
        IForObtainingRates rateProvider = new StubRateProviderAdapter();
        IForStoringTickets storingTickets = new FakeTicketStoreAdapter();
        IForPaying paymentService = new SpyPaymentServiceAdapter();

        ForParkingCarsTestDriver.Instance.AppConfigurator = new AppConfigurator(rateProvider, storingTickets, paymentService);
        ForParkingCarsTestDriver.Instance.CarParker = new CarParker(rateProvider, storingTickets, paymentService);

        ForParkingCarsTestDriver.Instance.AppConfigurator.InitRateProviderWith(dataTable.ToRatesList());
    }

    [When("I ask for getting all rates by name")]
    public void WhenIAskForGettingAllRatesByName()
    {
        Dictionary<string, Rate> ratesByName = ForParkingCarsTestDriver.Instance.CarParker.GetAllRatesByName();
        this.ratesByName = ratesByName;
    }

    [Then("I should obtain the following rates indexed by name:")]
    public void ThenIShouldObtainTheFollowingRatesIndexedByName(DataTable dataTable)
    {
        Dictionary<string, Rate> expectedRatesByName = dataTable.ToRatesDictionary();
        Assert.That(this.ratesByName, Is.EqualTo(expectedRatesByName).Using(new RateEqualityComparer()));
    }
}