namespace Aydinturk_agency.Models.ViewModels
{
    public class ReservationVM
    {
        public List<ReservationRequest> data { get; set; } 
            
        public int FlightId { get; set; }

        public string PhoneNumber { get; set; }
    }
}
