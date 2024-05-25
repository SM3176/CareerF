using System;
using System.Collections.Generic;

namespace CareerFIZ.Models
{
    public partial class AppUserToken
    {
        public Guid UserId { get; set; }
        public string? LoginProvider { get; set; }
        public string? Name { get; set; }
        public string? Value { get; set; }

        public virtual AppUser User { get; set; } = null!;
    }
}
