using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace project2FreelancerPortal.Models
{
    public class InsertAdmin
    {
        [Required(ErrorMessage ="Enter name")]
        public string name { set; get; }
        [Required(ErrorMessage = "Enter email")]
        [EmailAddress(ErrorMessage ="Enter valid email")]
        public string email { set; get; }
        [Required(ErrorMessage = "Enter phone number")]
        [RegularExpression(@"^(\d{10})$", ErrorMessage ="Enter valid phone")]
        public string phone { set; get; }

        [Required(ErrorMessage = "Enter username")]
        public string username { set; get; }
        [Required(ErrorMessage = "Enter password")]
        public string pass { set; get; }
        public string msg { set; get; }
    }
}