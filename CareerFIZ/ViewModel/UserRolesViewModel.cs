using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace CareerFIZ.ViewModel
{
    public class UserRolesViewModel
    {
        public Guid RoleId { get; set; }
        [Display(Name = "Role")]
        public string RoleName { get; set; }
        public bool IsSelected { get; set; }
    }
}
