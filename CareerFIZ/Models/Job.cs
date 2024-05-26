using System;
using System.Collections.Generic;

namespace CareerFIZ.Models
{
    public partial class Job
    {
        public Job()
        {
            Cvs = new HashSet<Cv>();
            Skills = new HashSet<Skill>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Slug { get; set; } = null!;
        public int? CategoryId { get; set; }
        public int TitleId { get; set; }
        public string? Description { get; set; }
        public string? Introduce { get; set; }
        public string? ObjectTarget { get; set; }
        public string? Experience { get; set; }
        public DateTime? CreateDate { get; set; }
        public int Popular { get; set; }
        public int ProvinceId { get; set; }
        public int TimeId { get; set; }
        public int? MinSalary { get; set; }
        public int? MaxSalary { get; set; }
        public Guid AppUserId { get; set; }
        public bool? IsSponser { get; set; }

        public virtual AppUser AppUser { get; set; } = null!;
        public virtual Category? Category { get; set; }
        public virtual Province Province { get; set; } = null!;
        public virtual Time Time { get; set; } = null!;
        public virtual Title Title { get; set; } = null!;
        public virtual ICollection<Cv> Cvs { get; set; }

        public virtual ICollection<Skill> Skills { get; set; }
    }
}
