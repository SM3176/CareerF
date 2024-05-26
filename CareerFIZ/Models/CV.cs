using System;
using System.Collections.Generic;

namespace CareerFIZ.Models
{
    public partial class Cv
    {
        public int Id { get; set; }
        public string Certificate { get; set; } = null!;
        public string Major { get; set; } = null!;
        public DateTime ApplyDate { get; set; }
        public string GraduatedAt { get; set; } = null!;
        public float Gpa { get; set; }
        public int JobId { get; set; }
        public string Description { get; set; } = null!;
        public string Introduce { get; set; } = null!;
        public Guid AppUserId { get; set; }
        public int Status { get; set; }
        public string? UrlImage { get; set; }
        public string Phone { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? EmployerAddress { get; set; }
        public string? EmployerPhone { get; set; }
        public string? Comment { get; set; }
        public byte? EmployerRating { get; set; }
        public DateTime? CommentOn { get; set; }
        public string? City { get; set; }
        public string? EmployerEmail { get; set; }

        public virtual AppUser AppUser { get; set; } = null!;
        public virtual Job Job { get; set; } = null!;
    }
}
