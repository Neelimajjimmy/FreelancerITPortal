using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace project2FreelancerPortal.Models
{
    public class Login
    {
        [Required(ErrorMessage ="Required")]
        public string username { set; get; }
        [Required(ErrorMessage = "Required")]
        public string pass { set; get; }
    }
}