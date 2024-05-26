using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

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
            RolesNavigation = new HashSet<AppRole>();
        }

        public Guid Id { get; set; }
        public string? FullName { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public int? Age { get; set; }
        public DateTime? CreateDate { get; set; }
        public string? UrlAvatar { get; set; }
        public string? Contact { get; set; }
        public string? Description { get; set; }
        public string? WebsiteUrl { get; set; }
        public string? Location { get; set; }
        public int? Status { get; set; }
        public string Slug { get; set; } = null!;
        public int? ProvinceId { get; set; }
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
        public string? CompanySize { get; set; }
        public string? WorkingDays { get; set; }
        public int? CountryId { get; set; }
        public string? Content { get; set; }
        public int Popular { get; set; }
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
        public virtual ICollection<AppRole> RolesNavigation { get; set; }
    }
}
