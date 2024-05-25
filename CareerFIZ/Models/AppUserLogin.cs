using System;
using System.Collections.Generic;

namespace CareerFIZ.Models
{
    public partial class AppUserLogin
    {
        public Guid UserId { get; set; }
        public string? LoginProvider { get; set; }
        public string? ProviderKey { get; set; }
        public string? ProviderDisplayName { get; set; }

        public virtual AppUser User { get; set; } = null!;
    }
}
