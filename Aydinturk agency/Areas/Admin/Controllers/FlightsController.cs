using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Aydinturk_agency.Data;
using Aydinturk_agency.Models;
using Aydinturk_agency.Models.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using Aydinturk_agency.Utils;

namespace Aydinturk_agency.Areas.Admin.Controllers
{
    [Area(SD.Admin_Role)]
    [Authorize(Roles = SD.Admin_Role)]
    public class FlightsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FlightsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/flights
        public async Task<IActionResult> Index()
        {
            return View();
        }


        // GET: Admin/Companies/Create
        public IActionResult Create()
        {
            FlightVM flightVM = new FlightVM()
            {
                // Get all companies as list items

                Companies = _context.Companies.ToList().Select
                (u => new SelectListItem { Text = u.Name, Value = u.Id.ToString() }),
                Flight = new Flight(),

                // Get all destinations as list items
                From = _context.Destinations.ToList().Select
                (u => new SelectListItem { Text = u.Name, Value = u.Id.ToString() }),
                To = _context.Destinations.ToList().Select
                (u => new SelectListItem { Text = u.Name, Value = u.Id.ToString() }),

            };



            return View(flightVM);
        }

        // POST: Admin/flights/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Flight flight)
        {

            FlightVM flightVM = new FlightVM();

            // Get all companies as list items

            flightVM.Companies = _context.Companies.ToList().Select(u => new SelectListItem { Text = u.Name, Value = u.Id.ToString() });

            flightVM.Flight = flight;

            // Get all destinations as list items
            flightVM.From = _context.Destinations.ToList().Select(u => new SelectListItem { Text = u.Name, Value = u.Id.ToString() });
            flightVM.To = _context.Destinations.ToList().Select(u => new SelectListItem { Text = u.Name, Value = u.Id.ToString() });


            if (flight.FromId == flight.ToId)
            {
                ModelState.AddModelError("destinationError", "وجهة الاياب يجب ان تكون مختلفة");
            }


            if (ModelState.IsValid)
            {

                _context.Flights.Add(flight);
                await _context.SaveChangesAsync();
                TempData["success"] = "تم نشر الرحلة";
                return RedirectToAction(nameof(Index));
            }
            return View(flightVM);
        }

        // GET: Admin/flights/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Flights == null)
            {
                return NotFound();
            }


            var flight = await _context.Flights.Include(f => f.Company).Include(f => f.From).Include(f => f.To).FirstOrDefaultAsync(f => f.Id == id);
            if (flight == null)
            {
                return NotFound();
            }

            FlightVM vm = new FlightVM()
            {
                Flight = flight,
                Companies = _context.Companies.ToList().Select
                (u => new SelectListItem { Text = u.Name, Value = u.Id.ToString() }),

                // Get all destinations as list items
                From = _context.Destinations.ToList().Select
                (u => new SelectListItem { Text = u.Name, Value = u.Id.ToString() }),
                To = _context.Destinations.ToList().Select
                (u => new SelectListItem { Text = u.Name, Value = u.Id.ToString() }),

            };



            return View(vm);
        }

        // POST: Admin/flights/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Flight flight)
        {
            if (id != flight.Id || id == 0)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Flights.Update(flight);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (FlightExists(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                TempData["success"] = "تم تعديل الرحلة";
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        private bool FlightExists(int id)
        {
            return _context.Flights.Any(e => e.Id == id);
        }

        #region API Calls

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var flights = await _context.Flights.Include(f => f.Company).Include(f => f.From).Include(f => f.To).ToListAsync();

            return Json(new { data = flights });

        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)

        {
            var flight = await _context.Flights.FindAsync(id);
            if (flight == null)
            {
                return Json(new { success = false, message = "حدث خطأ..." });
            }
            _context.Flights.Remove(flight);
            await _context.SaveChangesAsync();
            return Json(new { success = true, message = "تم حذف الرحلة" });
        }

        #endregion
    }
}
