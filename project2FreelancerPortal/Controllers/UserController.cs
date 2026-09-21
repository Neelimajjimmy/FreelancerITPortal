using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using project2FreelancerPortal.Models;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace project2FreelancerPortal.Controllers
{
    public class UserController : Controller
    {
        freelanceDbEntities db = new freelanceDbEntities();
        // GET: User
        public ActionResult userIndex()
        {
            var projectlist = db.sp_GetProject().Select(x => new Project
            {
                pid = x.projid,
                title = x.title,
                description = x.description,
                skill = x.skills,
                budget = x.budget,
                duration = x.duration,
                deadline = x.deadline,
                exp = x.explevel,
                status=x.projstatus
            }).ToList();
            ViewBag.projects = projectlist;
            return View();
        }

        public ActionResult createUser()
        {
            return View();
        }

        [HttpPost]
        public ActionResult createUser(userReg user,HttpPostedFileBase phFile,HttpPostedFileBase resumeFile)
        {
            if (ModelState.IsValid)
            {
                var getmaxid = db.sp_MaxLogId().FirstOrDefault();
                int mid = Convert.ToInt32(getmaxid);
                int regid = 0;
                if (mid == 0)
                {
                    regid = 1;
                }
                else
                {
                    regid = mid + 1;
                }

                if (phFile.ContentLength > 0)
                {
                    string fname = Path.GetFileName(phFile.FileName);
                    string s = Server.MapPath("~/PHS");
                    string sa = Path.Combine(s, fname);
                    phFile.SaveAs(sa); // to save file in folder

                    string fullPath = Path.Combine("~\\PHS", fname);
                    user.photo = fullPath; // set to property to save to db


                }

                if (resumeFile.ContentLength > 0)
                {
                    string fname = Path.GetFileName(resumeFile.FileName);
                    string s = Server.MapPath("~/Resume");
                    string sa = Path.Combine(s, fname);
                    resumeFile.SaveAs(sa); // to save file in folder

                    string fullPath = Path.Combine("~\\Resume", fname);
                    user.resume = fullPath; // set to property to save to db


                }
                db.sp_registerUser(regid, user.name, user.email, user.address, user.phone, user.skills, user.exp, user.resume, user.photo,"active");
                db.sp_LoginInsert(regid, user.username, user.pass, "user");
                user.msg = "User registered";
            }
            return View(user);
        }

        public ActionResult search_click(SearchProject sp)
        {
            string qry = "";
            if (!string.IsNullOrWhiteSpace(sp.skills))
            {
                qry += "  and skills like  '%" + sp.skills + "%'";
            }
            if (!string.IsNullOrWhiteSpace(sp.exp))
            {
                qry += "  and explevel like  '%" + sp.exp + "%'";
            }
            if (!string.IsNullOrWhiteSpace(sp.duration))
            {
                qry += "  and duration like  '%" + sp.duration + "%'";
            }
            var data = getProjects( qry);
            ViewBag.projects = data;
            return View("userIndex");

        }

        private List<Project> getProjects(string qry)
        {
            List<Project> projectlist = new List<Project>();
            using (var con=new SqlConnection(ConfigurationManager.ConnectionStrings["searchCon"].ConnectionString))
            {

                SqlCommand cmd = new SqlCommand("sp_SearchProjects", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@qry", qry);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    var o = new Project
                    {
                        pid = Convert.ToInt32(dr["projid"].ToString()),
                        title = dr["title"].ToString(),
                        description = dr["description"].ToString(),
                        skill= dr["skills"].ToString(),
                        budget = Convert.ToDecimal(dr["budget"].ToString()),
                        duration = Convert.ToInt32(dr["duration"].ToString()),
                        deadline = Convert.ToDateTime(dr["deadline"].ToString()),
                        exp = Convert.ToInt32(dr["explevel"].ToString()),
                        status = dr["projstatus"].ToString()

                    };
                    projectlist.Add(o);
                }
                con.Close();
                return projectlist;
            }
        }

        public ActionResult Bidnow(int id)
        {
            var getdata = db.sp_GetProjectDetails(id).FirstOrDefault();
            ProjectBid bid = new ProjectBid
            {
                prid = id,
                title = getdata.title,
                desc = getdata.description,
                skill = getdata.skills


            };
            return View(bid);
        }
        [HttpPost]
        public ActionResult Bidnow(ProjectBid bid)
        {
            bid.uid =Convert.ToInt32( Session["userid"]);
            db.sp_BidProject(bid.prid, bid.uid, bid.amt, bid.days, "bid");
            return RedirectToAction("userIndex");
        }

        [HttpGet]
        public ActionResult GetMyBids()
        {
            int id = Convert.ToInt32(Session["userid"]);
            var mybids = db.sp_GetProposals(id).ToList();
            ViewBag.myBids = mybids;
            return View();

        }

    }
}