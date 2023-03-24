using Aydinturk_agency.Data;
using Aydinturk_agency.Models;
using Aydinturk_agency.Models.ViewModels;
using Aydinturk_agency.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Aydinturk_agency.Controllers
{
    [Area(SD.Customer_Role)]
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _context = context;
        }

        [Route("/")]
        [Route("/home")]
        [Route("/index")]
        public IActionResult Index()
        {

            return View();
        }


        [Route("/customer/home")]
        [Route("/customer")]
        [Authorize]
        public async Task<IActionResult> Dashboard()
        {

            // prepare dashboard for customer

            CustomerDashboardVM dashboardVM = new CustomerDashboardVM
            {
                Flights = await _context.Flights.Include(f => f.Company).Include(f => f.To).Include(f => f.From).ToListAsync(),
                
            };
            

            return View(dashboardVM);
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}