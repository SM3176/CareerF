using System.ComponentModel.DataAnnotations;
using CareerFIZ.Common;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CareerFIZ.Models
{
    public class Payment
    {
        public int Id {  get; set; }

        [Precision(18, 2)]
        public decimal Amount { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime PaymentDate { get; set; }

        public Guid AppUserId { get; set; }

        public virtual AppUser AppUsers { get; set; }
    }
}
