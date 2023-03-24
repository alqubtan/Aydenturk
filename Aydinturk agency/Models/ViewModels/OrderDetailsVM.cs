namespace Aydinturk_agency.Models.ViewModels
{
    public class OrderDetailsVM
    {
        public OrderHeader OrderHeader { get; set; }
        public List<OrderDetails> OrderDetails { get; set; }
        public string OrderdBy { get; set; }
    }
}
