using System;
using System.Collections.Generic;

namespace CareerFIZ.Models
{
    public partial class Time
    {
        public Time()
        {
            Jobs = new HashSet<Job>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Slug { get; set; } = null!;
        public bool? Disable { get; set; }

        public virtual ICollection<Job> Jobs { get; set; }
    }
}
