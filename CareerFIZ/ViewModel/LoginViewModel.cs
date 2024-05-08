using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace CareerFIZ.ViewModel
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
        [Display(Name = "Remember")]
        public bool RememberMe { get; set; }
    }
}
