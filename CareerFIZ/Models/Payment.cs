using System;
using System.Collections.Generic;

namespace CareerFIZ.Models
{
    public partial class Payment
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public Guid AppUserId { get; set; }

        public virtual AppUser AppUser { get; set; } = null!;
    }
}
