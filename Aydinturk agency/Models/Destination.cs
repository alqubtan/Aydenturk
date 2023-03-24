using System.ComponentModel.DataAnnotations;

namespace Aydinturk_agency.Models
{
    public class Destination
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name = "الوجهة")]
        public string Name { get; set; }
    }
}
