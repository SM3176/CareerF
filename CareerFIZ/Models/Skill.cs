using System;
using System.Collections.Generic;

namespace CareerFIZ.Models
{
    public partial class Skill
    {
        public Skill()
        {
            Jobs = new HashSet<Job>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Slug { get; set; } = null!;
        public string Logo { get; set; } = null!;
        public int CategoryId { get; set; }
        public bool? Disable { get; set; }
        public int Popular { get; set; }

        public virtual Category Category { get; set; } = null!;

        public virtual ICollection<Job> Jobs { get; set; }
    }
}
