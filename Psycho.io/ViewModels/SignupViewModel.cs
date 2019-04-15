using System;
using System.ComponentModel.DataAnnotations;
using Psycho.DAL.Core.Domain;

namespace Psycho.io.ViewModels
{
    public class SignupViewModel
    {
        [Required(ErrorMessage = "Email is required")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "First name is required")]
        [Display(Name = "First name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        [Display(Name = "Last name")]
        public string LastName { get; set; }

        [Phone]
        public string Phone { get; set; }

        public string Gender { get; set; }

        public int? Age { get; set; }

        public int? Height { get; set; }

        public int? Weight { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        /*[DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        [Display(Name = "Submit password")]
        public string ConfirmPassword { get; set; }*/
    }
}
