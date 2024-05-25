using System;
using System.Collections.Generic;

namespace CareerFIZ.Models
{
    public partial class Country
    {
        public Country()
        {
            AppUsers = new HashSet<AppUser>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Flag { get; set; } = null!;
        public bool? Disable { get; set; }

        public virtual ICollection<AppUser> AppUsers { get; set; }
    }
}
