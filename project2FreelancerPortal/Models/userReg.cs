using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace project2FreelancerPortal.Models
{
   
    public class userReg
    {

        [Required(ErrorMessage = "Enter name")]
        public string name { set; get; }
        [Required(ErrorMessage = "Enter email")]
        [EmailAddress(ErrorMessage = "Enter valid email")]
        public string email { set; get; }

        [Required(ErrorMessage = "Enter address")]
        public string address { set; get; }

       
        [Required(ErrorMessage = "Enter phone number")]
        [RegularExpression(@"^(\d{10})$", ErrorMessage = "Enter valid phone")]
        public string phone { set; get; }

        public string skills { set; get; }

        [Required(ErrorMessage = "Enter experience")]
        public int exp { set; get; }

        public string resume { set; get; }
        public string photo { set; get; }

        [Required(ErrorMessage = "Enter username")]
        public string username { set; get; }
        [Required(ErrorMessage = "Enter password")]
        public string pass { set; get; }
        public string msg { set; get; }
    }
}