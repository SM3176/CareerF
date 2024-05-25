﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CareerFIZ.Models
{
    public partial class Time
    {
        public Time()
        {
            Jobs = new HashSet<Job>();
        }

        public int Id { get; set; }
        [Required(ErrorMessage = "Please enter type name")]
        [StringLength(20, ErrorMessage = "The type name cannot be more than 20 characters.")]
        public string Name { get; set; }
        [Required]
        public string Slug { get; set; }
        public bool? Disable { get; set; }

        public virtual ICollection<Job> Jobs { get; set; }
    }
}
