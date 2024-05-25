using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [Display(Name = "Name")]
        [Required(ErrorMessage = "Please enter product name.")]
        [StringLength(100, ErrorMessage = "Job name cannot be more than 100 characters.")]
        public string Name { get; set; }
        [Required]
        public string Slug { get; set; }
        [Display(Name = "Category")]
        public int? CategoryId { get; set; }
        [Display(Name = "Title")]
        public int TitleId { get; set; }
        [Display(Name = "Description")]
        public string? Description { get; set; }
        [Display(Name = "Introduce")]
        public string? Introduce { get; set; }
        [Display(Name = "Object target")]
        public string? ObjectTarget { get; set; }
        [Display(Name = "Work experience")]
        public string? Experience { get; set; }
        [Display(Name = "Create date")]
        public DateTime? CreateDate { get; set; }
        public int Popular { get; set; }
        [Display(Name = "Province")]
        public int ProvinceId { get; set; }
		public virtual Time Time { get; set; } = null!;
		[Display(Name = "Working type")]
        public int TimeId { get; set; }
        [Display(Name = "Min salary")]
        [Range(0, int.MaxValue, ErrorMessage = "Please enter valid salary.")]
        public int? MinSalary { get; set; }
        [Display(Name = "Max salary")]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter valid salary.")]
        //[SalaryRange("MinSalary")] //Salary Range Validation Attribute
        public int? MaxSalary { get; set; }
        public bool isSponser {  get; set; }
        [Display(Name = "Employer")]
        public Guid AppUserId { get; set; }
        public bool? IsSponser { get; set; }

        public virtual AppUser AppUser { get; set; } = null!;
        public virtual Category? Category { get; set; }
        public virtual Province Province { get; set; } = null!;
        
        public virtual Title Title { get; set; } = null!;
        public virtual ICollection<Cv> Cvs { get; set; }

        public virtual ICollection<Skill> Skills { get; set; }
    }
}
