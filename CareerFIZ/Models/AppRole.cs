using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace CareerFIZ.Models
{
    public partial class AppRole : IdentityRole<Guid>
    {
        public AppRole()
        {
            AppRoleClaims = new HashSet<AppRoleClaim>();
            Users = new HashSet<AppUser>();
            UsersNavigation = new HashSet<AppUser>();
        }

        public Guid Id { get; set; }
        public string? Description { get; set; }
        public string? Name { get; set; }
        public string? NormalizedName { get; set; }
        public string? ConcurrencyStamp { get; set; }

        public virtual ICollection<AppRoleClaim> AppRoleClaims { get; set; }

        public virtual ICollection<AppUser> Users { get; set; }
        public virtual ICollection<AppUser> UsersNavigation { get; set; }
    }
}
