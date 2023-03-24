using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aydinturk_agency.Models
{
    public class OrderDetails
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int OrderHeaderId { get; set; }
        [ForeignKey("OrderHeaderId")]
        [ValidateNever]
        public OrderHeader OrderHeader { get; set; }

        [Required(ErrorMessage = "حقل الاسم مطلوب")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "حقل الجنس مطلوب")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "حقل الفئة العمرية مطلوب")]
        public string AgeCategory { get; set; }

        [Required(ErrorMessage = "حقل البلد مطلوب")]

        public string Country { get; set; }

        [Required(ErrorMessage = "حقل رقم الجواز مطلوب")]

        public string PassportNumber { get; set; }
        [Required(ErrorMessage = "حقل تاريخ الميلاد مطلوب")]

        public string yearOfBirth { get; set; }
        
    }
}
