using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace project2FreelancerPortal.Models
{
    public class Project
    { 
        public int pid { set; get; }
        [Required(ErrorMessage ="Required")]
        public string  title { set; get; }
        [Required(ErrorMessage = "Required")]
        public string description { set; get; }
        [Required(ErrorMessage = "Required")]
        public string skill { set; get; }
        public decimal budget { set; get; }
        public int duration { set; get; }
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}",
           ApplyFormatInEditMode = true)]
        public DateTime deadline { set; get; }
        [Required(ErrorMessage = "Required")]
        public int exp { set; get; }
        public string status { set; get; }

        public string msg { set; get; }
    }
}