using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aydinturk_agency.Models
{
    public class OrderHeader
    {
        [Key]
        public int Id { get; set; }
        public string ApplicationUserId { get; set; }
        [ForeignKey("ApplicationUserId")]
        [ValidateNever]
        public ApplicationUser ApplicationUser { get; set; }
        [Required]
        public int FlightId { get; set; }
        [ForeignKey("FlightId")]
        [ValidateNever]
        public Flight Flight { get; set; }

        [Required]
        public string OrderDate { get; set; }
        public string? OrderStatus { get; set; }
        [Required]
        public decimal OrderTotal { get; set; }
        public string PhoneNumber { get; set; }

    }
}
