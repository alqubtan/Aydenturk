namespace Aydinturk_agency.Utils
{
    public class Notification
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public DateTime Date { get; set; }
        public string UserId { get; set; }
        public bool Seen { get; set; }
    }

}
