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
    public class CompaniesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public CompaniesController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Admin/Companies
        public async Task<IActionResult> Index()
        {
            return View();
        }


        // GET: Admin/Companies/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Companies/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Company company, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    string webRootPath = _webHostEnvironment.WebRootPath;
                    var upload = Path.Combine(webRootPath, @"images/companies");
                    var fileName = Guid.NewGuid().ToString();
                    var extention = Path.GetExtension(file.FileName).ToLower();

                    using (var fileStream = new FileStream(Path.Combine(upload, fileName + extention), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }

                    company.ImageUrl = @"\images\companies\" + fileName + extention;
                }
                _context.Add(company);
                await _context.SaveChangesAsync();
                TempData["success"] = "تم اضافة الشركة";
                return RedirectToAction(nameof(Index));
            }
            return View(company);
        }

        // GET: Admin/Companies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Companies == null)
            {
                return NotFound();
            }

            var company = await _context.Companies.FindAsync(id);
            if (company == null)
            {
                return NotFound();
            }
            return View(company);
        }

        // POST: Admin/Companies/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,Company company, IFormFile? file)
        {
            if (id != company.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var existedCompany = await _context.Companies.FindAsync(id);
                try
                {
                    //check if there is already an image for that obj'
                    if (file != null)
                    {
                        string webRootPath = _webHostEnvironment.WebRootPath;

                        var oldImage = Path.Combine(webRootPath, existedCompany.ImageUrl.TrimStart('\\'));

                        if (System.IO.File.Exists(oldImage))
                        {
                            System.IO.File.Delete(oldImage);
                        }
                        var upload = Path.Combine(webRootPath, @"images/companies");
                        var fileName = Guid.NewGuid().ToString();
                        var extention = Path.GetExtension(file.FileName).ToLower();

                        using (var fileStream = new FileStream(Path.Combine(upload, fileName + extention), FileMode.Create))
                        {
                            file.CopyTo(fileStream);
                        }

                        existedCompany.ImageUrl = @"\images\companies\" + fileName + extention;
                    }
                    // else tha image should be the same

                    existedCompany.Name = company.Name;
                    existedCompany.Description = company.Description;
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CompanyExists(company.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                TempData["success"] = "تم تعديل الشركة";
                return RedirectToAction(nameof(Index));
            }
            return View(company);
        }

        private bool CompanyExists(int id)
        {
            return _context.Companies.Any(e => e.Id == id);
        }

        #region API Calls

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var companies = await  _context.Companies.ToListAsync();

            return Json(new { data = companies });

        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)

        {
            var company = await _context.Companies.FindAsync(id);
            if (company == null)
            {
                return Json(new { success = false, message = "حدث خطأ..." });
            }
            // delete the image and product.
            if (company.ImageUrl != null)
            {
                var oldImage = Path.Combine(_webHostEnvironment.WebRootPath, company.ImageUrl.TrimStart('\\'));

                if (System.IO.File.Exists(oldImage))
                {
                    System.IO.File.Delete(oldImage);
                }
            }
             _context.Companies.Remove(company);
            await _context.SaveChangesAsync();
            return Json(new { success = true, message = "تم حذف الشركة" });
        }

        #endregion
    }
}
