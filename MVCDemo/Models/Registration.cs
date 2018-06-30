using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MVCDemo.Models
{
    [Table("RegistrationMain")]
    public class Registration
    {
        [Key]
        public int ID { get; set; }
        //validations 
        [Display(Name = "Name")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "FirstName is required")]
        public string Firstname { get; set; }
        [Display(Name = "Name")]
        [Required(AllowEmptyStrings =false, ErrorMessage = "LastName is required")]
        public string Lastname { get; set; }
       
        [Display(Name = "Username")]
        [Required(AllowEmptyStrings =false, ErrorMessage = "Username is required")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Username should be email id")]
        public string Email_username { get; set; }

        [Required(AllowEmptyStrings =false, ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "Minimum 6 characters is required")]
        public string Password { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Confirm password is required")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "Password do not match")]
        public string Confirmpassword { get; set; }

        public string Month { get; set; }
       
        public int Day { get; set; }
        [Display(Name = "Year")]
        
        public int year { get; set; }

        public string Gender { get; set; }

        public string Country { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "security question is required")]
        [Display(Name = "Select a security question")]
        public string Question { get; set; }

        public string Answer { get; set; }

    }
}