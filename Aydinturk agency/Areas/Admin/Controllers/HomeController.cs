using Aydinturk_agency.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Aydinturk_agency.Areas.Admin.Controllers
{
    [Area(SD.Admin_Role)]
    [Authorize(Roles = SD.Admin_Role)]
    public class HomeController : Controller
    {
        [Route("/admin/home")]
        [Route("/admin")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
