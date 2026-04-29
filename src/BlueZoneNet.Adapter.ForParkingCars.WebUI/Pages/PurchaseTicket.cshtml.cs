using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BlueZoneNet.Adapter.ForParkingCars.WebUI.Pages
{
    public class PurchaseTicketModel : PageModel
    {
        [BindProperty]
        public string TicketCode { get; set; } = default!;

        public void OnGet()
        {
        }

        public void OnPost()
        {
        }
    }
}
