using System;
using System.Collections.Generic;

namespace CareerFIZ.Models
{
    public partial class AppRoleClaim
    {
        public int Id { get; set; }
        public Guid RoleId { get; set; }
        public string? ClaimType { get; set; }
        public string? ClaimValue { get; set; }

        public virtual AppRole Role { get; set; } = null!;
    }
}
