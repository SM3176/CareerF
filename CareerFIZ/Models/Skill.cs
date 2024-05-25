using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace CareerFIZ.Models
{
    public partial class Skill
    {
        public Skill()
        {
            Jobs = new HashSet<Job>();
        }

        public int Id { get; set; }
        [Required(ErrorMessage = "Please enter skill name")]
        [StringLength(50, ErrorMessage = "The skill name cannot be more than 50 characters.")]
        public string Name { get; set; }
        [Required]
        public string Slug { get; set; }
        [Display(Name = "Skill logo")]
        [Required(ErrorMessage = "Please enter skill logo")]
        public string Logo { get; set; }
        [Display(Name = "Category")]
        public int CategoryId { get; set; }
        public bool? Disable { get; set; }
        public int Popular { get; set; }

        public virtual Category Category { get; set; } = null!;

        public virtual ICollection<Job> Jobs { get; set; }
    }
}
