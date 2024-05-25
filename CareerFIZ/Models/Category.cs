using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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
        [Display(Name = "Name")]
        [Required(ErrorMessage = "Please enter category name")]
        [StringLength(100, ErrorMessage = "Category name cannot be more than 100 characters.")]
        public string Name { get; set; }
        [Display(Name = "Description")]
        [StringLength(256, ErrorMessage = "The description cannot be more than 256 characters.")]
        public string? Description { get; set; }
        [Required]
        public string Slug { get; set; }
        public bool? Disable { get; set; }

        public virtual ICollection<AppUser> AppUsers { get; set; }
        public virtual ICollection<Job> Jobs { get; set; }
        public virtual ICollection<Province> Provinces { get; set; }
        public virtual ICollection<Skill> Skills { get; set; }
        public virtual ICollection<Title> Titles { get; set; }
    }
}
