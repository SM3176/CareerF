using System;
using System.Collections.Generic;

namespace CareerFIZ.Models
{
    public partial class Province
    {
        public Province()
        {
            AppUsers = new HashSet<AppUser>();
            Jobs = new HashSet<Job>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Slug { get; set; } = null!;
        public int CategoryId { get; set; }
        public bool? Disable { get; set; }
        public int Popular { get; set; }

        public virtual Category Category { get; set; } = null!;
        public virtual ICollection<AppUser> AppUsers { get; set; }
        public virtual ICollection<Job> Jobs { get; set; }
    }
}
