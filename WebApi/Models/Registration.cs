using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApi.Models
{
    [Table("RegistrationMain")]
    public class Registration
    {
        [Key]
        public int ID { get; set; }
        [Required(ErrorMessage = "FirstName is required")]
        [Display(Name = "Name")]
        public string Firstname { get; set; }
        [Required(ErrorMessage = "LastName is required")]
        public string Lastname { get; set; }
       
        [Display(Name = "Username")]
        [Required(ErrorMessage = "Username is required")]
        public string Email_username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Confirm password is required")]
        [Display(Name = "Confirm password")]
        public string Confirmpassword { get; set; }

        public string Month { get; set; }

        public int Day { get; set; }
        [Display(Name = "Year")]
        public int year { get; set; }

        public string Gender { get; set; }

        public string Country { get; set; }
        [Required(ErrorMessage = "security question is required")]
        [Display(Name = "Select a security question")]
        public string Question { get; set; }

        public string Answer { get; set; }

    }
}