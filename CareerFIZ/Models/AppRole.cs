using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace CareerFIZ.Models
{
    public class AppRole : IdentityRole<Guid>
    {
        [Display(Name = "Description")]
        [StringLength(256, ErrorMessage = "The description cannot be more than 256 characters.")]
        public string? Description { get; set; }
    }
}