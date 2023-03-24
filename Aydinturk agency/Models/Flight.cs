using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aydinturk_agency.Models
{
    public class Flight

    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "الشركة")]
        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        public int CompanyId { get; set; }
        public Company? Company { get; set; }

        [Display(Name = "من")]
        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        public int FromId { get; set; }
        public Destination? From { get; set; }

        [Display(Name = "الى")]
        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        public int ToId { get; set; }
        public Destination? To { get; set; }

        [Display(Name = "التاريخ")]
        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy/mm/dd}", ApplyFormatInEditMode = true)]
        public string Date { get; set; }
        [Display(Name = "الوقت")]
        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        [DataType(DataType.Time)]
        public string Time { get; set; }
        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        [Display(Name = "عدد المقاعد")]
        [Range(1, 100, ErrorMessage = "الحد الادنى للمقاعد هو 1 والحد الاعلى هو 100")]
        public int Sets { get; set; }
        [Required(ErrorMessage = "هذا الحقل مطلوب")]
        [Display(Name = "الفئة")]
        public string Category { get; set; }
        [Display(Name = "الوزن (كيلوجرام)")]
        [Range(1, 100, ErrorMessage = "الحد الادنى للاوزان هو 1 والحد الاعلى هو 100")]
        public int Weight { get; set; }

        [Display(Name = "السعر (دولار)")]
        [Range(1, 1000, ErrorMessage ="الحد الادنى هو 0 والحد الاعلى هو 1000")]
        public decimal Price { get; set; }

        [Display(Name = "السعر للطفل (دولار)")]
        [Range(1, 1000, ErrorMessage = "الحد الادنى هو 0 والحد الاعلى هو 1000")]
        public decimal PriceForKid { get; set; }

        [Display(Name = "السعر للرضيع (دولار)")]
        [Range(1, 1000, ErrorMessage = "الحد الادنى هو 0 والحد الاعلى هو 1000")]
        public decimal PriceForBaby { get; set; }


    }
}


