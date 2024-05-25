using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace CareerFIZ.Models
{
    public partial class Log
    {
        public int Id { get; set; }
        public string Action { get; set; } = null!;
        public DateTime ActionTime { get; set; }
        public string Ipaddress { get; set; } = null!;
        public Guid AppUserId { get; set; }

        public virtual AppUser AppUser { get; set; } = null!;
    }
}
