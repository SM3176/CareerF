using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CareerFIZ.Models
{
    public partial class AppUser : IdentityUser<Guid>
	{
        public AppUser()
        {
            AppUserClaims = new HashSet<AppUserClaim>();
            Cvs = new HashSet<Cv>();
            Jobs = new HashSet<Job>();
            Logs = new HashSet<Log>();
            Payments = new HashSet<Payment>();
            Roles = new HashSet<AppRole>();
        }

        public Guid Id { get; set; }
		//User
		[Display(Name = "Full name")]
		[StringLength(100, ErrorMessage = "Full name cannot be more than 100 characters.")]
		public string? FullName { get; set; }
        [Display(Name = "Phone")]
        [StringLength(20, ErrorMessage = "Please enter valid phonenumber.", MinimumLength = 9)]
        public string? Phone { get; set; }
        [Display(Name = "Address")]
        public string? Address { get; set; }
        [Display(Name = "Age")]
        [Range(0, 100, ErrorMessage = "Please enter valid age.")]
        public int? Age { get; set; }
        [Display(Name = "Create date")]
        public DateTime? CreateDate { get; set; }
        [Display(Name = "Logo")]
        public string? UrlAvatar { get; set; }
        //Employer
        [Display(Name = "Contact")]
        public string? Contact { get; set; }
        [Display(Name = "Company overview")]
        public string? Description { get; set; }
        [Display(Name = "Website")]
        [StringLength(50, ErrorMessage = "The website cannot be more than 50 characters.")]
        public string? WebsiteURL { get; set; }
        [Display(Name = "Location")]
        public string? Location { get; set; }
        public int? Status { get; set; }
        [Required]
        public string Slug { get; set; } = null!;
        public int? ProvinceId { get; set; }
		[Display(Name = "Province")]
		public bool? Disable { get; set; }
        public int? CategoryId { get; set; }
        public string? UserName { get; set; }
        public string? NormalizedUserName { get; set; }
        public string? Email { get; set; }
        public string? NormalizedEmail { get; set; }
        public bool EmailConfirmed { get; set; }
        public string? PasswordHash { get; set; }
        public string? SecurityStamp { get; set; }
        public string? ConcurrencyStamp { get; set; }
        public string? PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public DateTimeOffset? LockoutEnd { get; set; }
        public bool LockoutEnabled { get; set; }
        public int AccessFailedCount { get; set; }
		[Display(Name = "Company size")]
		public string? CompanySize { get; set; }
        [Display(Name = "Working days")]
        public string? WorkingDays { get; set; }
        public Country? Country { get; set; }
        [Display(Name = "Country")]
        public int? CountryId { get; set; }
        [Display(Name = "Content")]
        public string? Content { get; set; }
        public int Popular { get; set; }
        [DefaultValue(0)]
        public int VipLv { get; set; }

        public virtual Category? Category { get; set; }
        public virtual Country? Country { get; set; }
        public virtual Province? Province { get; set; }
        public virtual AppUserLogin? AppUserLogin { get; set; }
        public virtual AppUserToken? AppUserToken { get; set; }
        public virtual ICollection<AppUserClaim> AppUserClaims { get; set; }
        public virtual ICollection<Cv> Cvs { get; set; }
        public virtual ICollection<Job> Jobs { get; set; }
        public virtual ICollection<Log> Logs { get; set; }
        public virtual ICollection<Payment> Payments { get; set; }

        public virtual ICollection<AppRole> Roles { get; set; }
    }
}
