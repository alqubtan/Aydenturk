using Aydinturk_agency.Data;
using Aydinturk_agency.Models;
using Aydinturk_agency.Models.ViewModels;
using Aydinturk_agency.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Aydinturk_agency.Areas.Customer.Controllers
{
    [Area(SD.Customer_Role)]
    [Authorize]
    public class OrdersController : Controller
    {
        private readonly ApplicationDbContext _context;
        public OrdersController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> StepOne([FromQuery] int selection)
        {
            var flight = await _context.Flights
                .Include(f => f.Company)
                .Include(f => f.From)
                .Include(f => f.To).FirstOrDefaultAsync(f => f.Id == selection);

            if (flight == null) return NotFound();

            OrderStep1VM orderStep1VM = new OrderStep1VM()
            {
                Flight = flight,
                AdultSetsNumber = 5,
                KidsSetsNumber = 5,
                BabySetsNumber = 5
            };
            return View(orderStep1VM);
        }





        [HttpGet]
        public async Task<IActionResult> StepTwo(

            [FromQuery] int adult,
            [FromQuery] int kids,
            [FromQuery] int baby,
            [FromQuery] int selection

            )

        {

            var flight = await _context.Flights
                .Include(f => f.Company)
                .Include(f => f.From)
                .Include(f => f.To).FirstOrDefaultAsync(f => f.Id == selection);

            if (flight == null) return NotFound();


            OrderStep2VM orderStep2VM = new OrderStep2VM()
            {
                Flight = flight,

            };

            if (adult == 0 & kids == 0 & baby == 0)
            {
                orderStep2VM.ErrorMSG = "يرجى تحديد عدد المقاعد";
                return View(orderStep2VM);
            }

            var totalSets = adult + kids + baby;

            if (totalSets > flight.Sets)
            {
                orderStep2VM.ErrorMSG = "عدد المقاعد لا يكفي";
                return View(orderStep2VM);
            }

            ApplicationUser user = await _context.ApplicationUsers.FirstOrDefaultAsync(u => u.Email == User.Identity.Name);

            var totalPrice = (flight.Price * adult) + (flight.PriceForKid * kids) + (flight.PriceForBaby * baby);

            if (totalPrice > user.AccountBalance)
            {
                orderStep2VM.ErrorMSG = "الرصيد لا يكفي";
                return View(orderStep2VM);
            }



            orderStep2VM.AdultReservations = adult;
            orderStep2VM.KidsReservations = kids;
            orderStep2VM.BabyReservations = baby;




            return View(orderStep2VM);
        }


        [HttpPost]
        public async Task<IActionResult> Reservation(ReservationVM vm)

        {

            var ReturnMSG = "يرجى ادخال بيانات الحجز";

            // check if there is data

            if (vm.data == null || vm.data.Count == 0)
            {

                return Json(new { success = false, message = ReturnMSG });
            }

            try
            {
                var claimsIdentity = (ClaimsIdentity)User.Identity;
                var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

                // Create Order Header

                OrderHeader orderHeader = new OrderHeader
                {
                    OrderDate = DateTime.UtcNow.AddHours(3).ToShortDateString(),
                    OrderStatus = SD.StatusPending,
                    ApplicationUserId = claim.Value,
                    FlightId = vm.FlightId,
                    PhoneNumber = vm.PhoneNumber,

                };

                _context.OrderHeaders.Add(orderHeader);
                await _context.SaveChangesAsync();

                // cacl order total

                var flight = await _context.Flights.FirstOrDefaultAsync(f => f.Id == vm.FlightId);

                if (flight == null)
                {
                    return NotFound();
                }

                var adultNumber = 0;
                var kidsNumber = 0;
                var babyNumber = 0;

                // Create Order Deetails

                vm.data.ForEach(res =>
                {
                    if (res.AgeCategory == "adult")
                    {
                        adultNumber++;
                    }
                    else if (res.AgeCategory == "kid")
                    {
                        kidsNumber++;
                    }
                    else if (res.AgeCategory == "baby")
                    {
                        babyNumber++;
                    }
                    var orderDetail = new OrderDetails
                    {
                        FullName = res.FullName,
                        Gender = res.Gender,
                        AgeCategory = res.AgeCategory,
                        Country = res.Country,
                        yearOfBirth = res.YearOfBirth,
                        PassportNumber = res.PassportNumber,
                        OrderHeaderId = orderHeader.Id,

                    };

                    _context.OrderDetails.Add(orderDetail);
                    


                });

                

                // calc order header total
                var orderTotal = (flight.Price * adultNumber) + (flight.PriceForKid * kidsNumber) + (flight.PriceForBaby * babyNumber);
                orderHeader.OrderTotal = orderTotal;
                

                
                await _context.SaveChangesAsync();

                ReturnMSG = "سيتم الاتصال بك واستقطاع المبلغ عند تأكيد الحجز";
                return Json(new { success = true, message = ReturnMSG });
            }
            catch (Exception ex)
            {
                ReturnMSG = "حدث خطأ, يرجى اعادة المحاولة";
                return Json(new { success = false, message = ReturnMSG });

            }



        }

        public async Task<IActionResult> GetOrders()
        {
            return View();
        }

        #region API Calls


        public async Task<IActionResult> GetUserOrders()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            var orders = await _context.OrderHeaders.Include(o => o.Flight).ToListAsync();

            return Json(new {data = orders});
        }

        #endregion
    }
}
