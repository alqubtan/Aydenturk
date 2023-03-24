using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace Aydinturk_agency.Models
{
    public class Company
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage ="اسم الشركة مطلوب")]
        [Display(Name ="اسم الشركة")]
        public string Name { get; set; }
        [Display(Name ="وصف الشركة (اختياري)")]
        [MaxLength(200, ErrorMessage ="الحد الاعلى للحروف هو 200 حرف")]
        public string? Description { get; set; }

        [Display(Name="شعار الشركة")]
        [ValidateNever]
        public string? ImageUrl { get; set; }
    }
}
