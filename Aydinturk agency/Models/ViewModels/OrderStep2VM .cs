using Microsoft.AspNetCore.Mvc;

namespace Aydinturk_agency.Models.ViewModels
{
    public class OrderStep2VM
    {
        public Flight Flight { get; set; }
        public int AdultReservations { get; set; }
        public int KidsReservations { get; set; }
        public int BabyReservations { get; set; }
        public string ErrorMSG { get; set; }


    }
}
