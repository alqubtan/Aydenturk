using Aydinturk_agency.Data;
using Aydinturk_agency.Models.ViewModels;
using Aydinturk_agency.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Aydinturk_agency.Areas.Admin.Controllers
{
    [Authorize(Roles = SD.Admin_Role)]
    [Area(SD.Admin_Role)]
    public class OrdersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OrdersController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Confirmation(int id)
        {


            var orderHeader = await _context.OrderHeaders.Include(o => o.Flight).FirstOrDefaultAsync(o => o.Id == id);

            if (orderHeader == null) return NotFound();

            OrderDetailsVM vm = new OrderDetailsVM
            {
                OrderHeader = orderHeader,
                OrderDetails = await _context.OrderDetails.Where(o => o.OrderHeaderId == id).ToListAsync(),

            };
            var customer = await _context.ApplicationUsers.FirstOrDefaultAsync(u => u.Id == orderHeader.ApplicationUserId);
            vm.OrderdBy = customer.FullName;
            return View(vm);

        }

        [HttpPost]
        public async Task<IActionResult> Confirmation(int id, string actionStatus)
        {

            var orderHeader = await _context.OrderHeaders.Include(o => o.Flight).FirstOrDefaultAsync(o => o.Id == id);

            if (orderHeader == null) return NotFound();

            // 1- change  order Header  to approved or rejected 

            if (actionStatus == SD.StatusApproved)
            {
                // Set status to Approved
                orderHeader.OrderStatus = SD.StatusApproved;
                // Decrement the customer's balance
                var customer = _context.ApplicationUsers.FirstOrDefault(c => c.Id == orderHeader.ApplicationUserId);
                customer.AccountBalance -= orderHeader.OrderTotal;
                await _context.SaveChangesAsync();
                TempData["success"] = "تم قبول الطلب, سيتم اشعار المستخدم تلقائيا.";

                // Send notification to customer that their order was approved
                var notification = new Notification
                {
                    Message = "تم قبول الحجز واستقطاع المبلغ بنجاح",
                    Date = DateTime.Now,
                    UserId = orderHeader.ApplicationUserId,
                    Seen = false
                };
                _context.Notifications.Add(notification);
                await _context.SaveChangesAsync();
            }
            else if (actionStatus == SD.StatusRejected)
            {
                // Set status to Rejected
                orderHeader.OrderStatus = SD.StatusRejected;
                await _context.SaveChangesAsync();
                // Send notification to customer that their order was rejected
                //SendNotification(order.CustomerId, "Your order has been rejected.");
                TempData["success"] = "تم رفض الطلب, سيتم اشعار المستخدم تلقائيا";
            }


            return Redirect(Request.Headers["Referer"].ToString());


        }
        #region Api Calls
        public async Task<IActionResult> GetAll()
        {


            var orders = await _context.OrderHeaders.Include(o => o.Flight).ToListAsync();

            return Json(new { data = orders });
        }
        #endregion

    }
}
