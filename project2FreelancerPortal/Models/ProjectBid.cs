using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace project2FreelancerPortal.Models
{
    public class ProjectBid
    {
        public int prid { get; set; }
        public int uid { get; set; }
        public string title { get; set; }
        public string desc { get; set; }
        public string skill { get; set; }

        public decimal amt { get; set; }
        public int days { get; set; }
    }
}