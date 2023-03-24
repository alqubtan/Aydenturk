using Microsoft.AspNetCore.Mvc.Rendering;

namespace Aydinturk_agency.Models.ViewModels
{
    public class FlightVM
    {

        public Flight Flight { get; set; }
        public IEnumerable<SelectListItem> Companies { get; set; }
        public IEnumerable<SelectListItem> From { get; set; }
        public IEnumerable<SelectListItem> To { get; set; }
    }
}
