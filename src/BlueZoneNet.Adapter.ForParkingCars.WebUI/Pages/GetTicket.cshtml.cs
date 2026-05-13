using BlueZoneNet.Hexagon.Ports.Driven.ForObtainingRates;
using BlueZoneNet.Hexagon.Ports.Driven.ForStoringTickets;
using BlueZoneNet.Hexagon.Ports.Driving.ForParkingCars;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BlueZoneNet.Adapter.ForParkingCars.WebUI.Pages
{
    public class GetTicketModel : PageModel
    {
        private readonly IForParkingCars _carParker;

        public Ticket? Ticket { get; set; } = default!;

        public Rate? TicketRate { get; set; } = default!;

        [BindProperty]
        public string TicketCode { get; set; } = default!;

        public GetTicketModel(IForParkingCars carParker)
        {
            _carParker = carParker;
        }

        public void OnPost()
        {
            Ticket = _carParker.GetTicket(TicketCode);

            if (Ticket is not null)
            {
                TicketRate = _carParker.GetAllRatesByName()[Ticket.RateName];
            }
        }
    }
}
