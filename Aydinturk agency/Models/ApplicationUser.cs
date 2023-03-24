using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Aydinturk_agency.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }
        public string Location { get; set; }

        public decimal AccountBalance { get; set; }

    }
}
