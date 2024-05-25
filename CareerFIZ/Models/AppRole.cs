using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Xml.Linq;

namespace CareerFIZ.Models
{
    public partial class AppRole : IdentityRole<Guid>
	{
        public AppRole()
        {

            Users = new HashSet<AppUser>();
        }

        public Guid Id { get; set; }
		[Display(Name = "Description")]
		[StringLength(256, ErrorMessage = "The description cannot be more than 256 characters.")]
		public string? Description { get; set; }
        public string? Name { get; set; }
        public string? NormalizedName { get; set; }
        public string? ConcurrencyStamp { get; set; }
        public virtual ICollection<AppUser> Users { get; set; }
    }
}
