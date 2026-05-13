using BlueZoneNet.Hexagon.Ports.Driving.ForParkingCars;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BlueZoneNet.Adapter.ForParkingCars.WebUI.Pages
{
    public class PurchaseTicketModel : PageModel
    {
        private readonly IForParkingCars _carParker;

        public string TicketCode { get; set; } = default!;

        [BindProperty]
        public string CarPlate { get; set; } = default!;

        [BindProperty]
        public string RateName { get; set; } = default!;

        [BindProperty]
        public double Amount { get; set; }

        [BindProperty]
        public string PaymentCard { get; set; } = default!;

        public PurchaseTicketModel(IForParkingCars carParker)
        {
            _carParker = carParker;
        }

        public void OnPost()
        {
            PurchaseTicketRequest purchaseTicketRequest = new()
            {
                Amount = this.Amount,
                CarPlate = this.CarPlate,
                Clock = DateTime.Now,
                PaymentCard = this.PaymentCard,
                RateName = this.RateName
            };

            TicketCode = _carParker.PurchaseTicket(purchaseTicketRequest);
        }
    }
}
