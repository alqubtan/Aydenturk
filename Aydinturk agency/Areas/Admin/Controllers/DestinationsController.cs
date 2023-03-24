using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Aydinturk_agency.Data;
using Aydinturk_agency.Models;
using Microsoft.AspNetCore.Authorization;
using Aydinturk_agency.Utils;

namespace Aydinturk_agency.Areas.Admin.Controllers
{
    [Area(SD.Admin_Role)]
    [Authorize(Roles = SD.Admin_Role)]
    public class DestinationsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public DestinationsController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Admin/Destinations
        public async Task<IActionResult> Index()
        {
            return View();
        }


        // GET: Admin/Destinations/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Destinations/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Destination destination)
        {
            if (ModelState.IsValid)
            {
                _context.Destinations.Add(destination);
                await _context.SaveChangesAsync();
                TempData["success"] = "تم اضافة الوجهة";
                return RedirectToAction(nameof(Index));
            }
            return View(destination);
        }

        // GET: Admin/destinations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Destinations == null)
            {
                return NotFound();
            }

            var destination = await _context.Destinations.FindAsync(id);
            if (destination == null)
            {
                return NotFound();
            }
            return View(destination);
        }

        // POST: Admin/destinations/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,Destination destination)
        {
            if (id != destination.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var existedDestination = await _context.Destinations.FindAsync(id);
                try
                {
                    existedDestination.Name = destination.Name;
                    await _context.SaveChangesAsync();
                    TempData["success"] = "تم تعديل الوجهة";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DestinationExists(destination.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(destination);
        }

        private bool DestinationExists(int id)
        {
            return _context.Destinations.Any(e => e.Id == id);
        }

        #region API Calls

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var destinations = await  _context.Destinations.ToListAsync();

            return Json(new { data = destinations });

        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)

        {
            var destination = await _context.Destinations.FindAsync(id);
            if (destination == null)
            {
                return Json(new { success = false, message = "حدث خطأ..." });
            }
            
             _context.Destinations.Remove(destination);
            await _context.SaveChangesAsync();
            return Json(new { success = true, message = "تم حذف الوجهة" });
        }

        #endregion
    }
}
