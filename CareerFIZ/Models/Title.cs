using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CareerFIZ.Models
{
    public partial class Title
    {
        public Title()
        {
            Jobs = new HashSet<Job>();
        }

        public int Id { get; set; }
        [Required(ErrorMessage = "Please enter title name")]
        [StringLength(100, ErrorMessage = "The title name cannot be more than 100 characters.")]
        public string Name { get; set; }
        [Display(Name = "Category")]
        public int CategoryId { get; set; }
        public bool? Disable { get; set; }
        [Required]
        public string Slug { get; set; } = null!;
        public int Popular { get; set; }

        public virtual Category Category { get; set; } = null!;
        public virtual ICollection<Job> Jobs { get; set; }
    }
}
