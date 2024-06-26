﻿using System.ComponentModel.DataAnnotations;

namespace CareerFIZ.ViewModel
{
    public class CreateRoleViewModel
    {
        [Required]
        [Display(Name = "Role")]
        public string RoleName { get; set; }
        [Required]
        [Display(Name = "Description")]
        public string RoleDescription { get; set; }
    }
}