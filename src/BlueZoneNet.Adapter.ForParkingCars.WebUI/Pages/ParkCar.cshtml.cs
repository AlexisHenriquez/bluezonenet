using BlueZoneNet.Hexagon.Ports.Driven.ForObtainingRates;
using BlueZoneNet.Hexagon.Ports.Driving.ForParkingCars;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BlueZoneNet.Adapter.ForParkingCars.WebUI.Pages
{
    public class ParkCarModel : PageModel
    {
        private readonly IForParkingCars _forParkingCars;
        public Dictionary<string, Rate> Rates { get; set; } = default!;

        public ParkCarModel(IForParkingCars forParkingCars)
        {
            _forParkingCars = forParkingCars;
        }

        public void OnGet()
        {
            Rates = _forParkingCars.GetAllRatesByName();
        }
    }
}
