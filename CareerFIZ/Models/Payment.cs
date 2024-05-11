using System.ComponentModel.DataAnnotations;
using CareerFIZ.Common;
using System;

namespace CareerFIZ.Models
{
    public class Payment
    {
        public int Id {  get; set; }

        public decimal Amount { get; set; }

        public DateTime PaymentDate { get; set; }

        public Guid AppUserId { get; set; }

        public virtual AppUser AppUsers { get; set; }
    }
}
