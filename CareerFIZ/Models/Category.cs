using System;
using System.Collections.Generic;

namespace CareerFIZ.Models
{
    public partial class Category
    {
        public Category()
        {
            AppUsers = new HashSet<AppUser>();
            Jobs = new HashSet<Job>();
            Provinces = new HashSet<Province>();
            Skills = new HashSet<Skill>();
            Titles = new HashSet<Title>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public string Slug { get; set; } = null!;
        public bool? Disable { get; set; }

        public virtual ICollection<AppUser> AppUsers { get; set; }
        public virtual ICollection<Job> Jobs { get; set; }
        public virtual ICollection<Province> Provinces { get; set; }
        public virtual ICollection<Skill> Skills { get; set; }
        public virtual ICollection<Title> Titles { get; set; }
    }
}
