﻿using System;
using System.ComponentModel.DataAnnotations;

namespace Psycho.io.ViewModels
{
    public class SigninViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
         
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
         
        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
         
        public string ReturnUrl { get; set; }
    }
}
