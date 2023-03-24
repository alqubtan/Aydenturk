using Aydinturk_agency.Data;
using Aydinturk_agency.Models.ViewModels;
using Aydinturk_agency.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Aydinturk_agency.Areas.Admin.Controllers
{
    [Area(SD.Admin_Role)]
    [Authorize(Roles = SD.Admin_Role)]
    
    public class AccountsController : Controller
    {
        private readonly ApplicationDbContext _context;
        public AccountsController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }


        public async Task<IActionResult> Edit([FromRoute] string id)
        {

            var account = await _context.ApplicationUsers.FirstOrDefaultAsync(acc => acc.Id == id);

            if (account == null)
            {
                return NotFound();
            }
            return View(account);


        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, EditAccountVM vm)
        {
            if (id == null)
            {
                return NotFound();
            }

            var existedAccount = await _context.ApplicationUsers.FindAsync(id);
            try
            {
                existedAccount.FullName = vm.FullName;
                existedAccount.AccountBalance = vm.AccountBalance;
                existedAccount.PhoneNumber = vm.PhoneNumber;
                existedAccount.Email = vm.Email;
                existedAccount.UserName = vm.Email;
                existedAccount.NormalizedEmail = vm.Email.ToUpper();
                existedAccount.NormalizedUserName = vm.Email.ToUpper();
                existedAccount.Location = vm.Location;


                await _context.SaveChangesAsync();
                TempData["success"] = "تم تعديل المعلومات";
            }
            catch (DbUpdateConcurrencyException)
            {
                if (_context.ApplicationUsers.Find(id) == null)
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





        #region API Calls
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var accounts = await _context.ApplicationUsers.ToListAsync();

            return Json(new { data = accounts });

        }

        
        [HttpDelete]
        public async Task<IActionResult> Delete([FromRoute]string id)

        {
          
            var account = await _context.ApplicationUsers.FindAsync(id);

            if(account == null)
            {
                return Json(new { success = false, message = "حدث خطا ..." });

            }
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            if (account.Id == claim.Value)
            {
                return Json(new { success = false, message = "لا يمكن حذف حسابك لأنك مدير النظام" });
            }
           _context.ApplicationUsers.Remove(account);
            await _context.SaveChangesAsync();
            return Json(new { success = true, message="تم حذف الحساب" });
        }
        #endregion
    }
}
