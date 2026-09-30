using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using project2FreelancerPortal.Models;

namespace project2FreelancerPortal.Controllers
{
    public class AdminController : Controller
    {
        freelanceDbEntities db = new freelanceDbEntities();
        // GET: Admin
        public ActionResult Index()
     {

            var projectlist = db.sp_GetProject().ToList();
            ViewBag.projects = projectlist;
            return View();
        }

        public ActionResult adminReg()
        {
            return View();
        }
        [HttpPost]
        public ActionResult adminReg(InsertAdmin clsob)
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

                db.sp_registerAdmin(regid, clsob.name, clsob.email, clsob.phone);
                db.sp_LoginInsert(regid, clsob.username, clsob.pass, "admin");
                clsob.msg = "sucessfully inserted";
                return View("adminReg", clsob);

            }
            return View("adminReg");
        }

        public ActionResult createProject()
        {
            return View();
        }

        [HttpPost]
        public ActionResult createProject(Project pj)
        {
            if (ModelState.IsValid)
            {
                db.sp_InsertProject(pj.title, pj.description, pj.skill, pj.budget, pj.duration, pj.deadline, pj.exp, "active");
                pj.msg = "Project created";
                return RedirectToAction("Index", pj);
            }
            return View();
        }

        public ActionResult editProject(int id)
        {
            var getdata = db.sp_GetProject().Where(x => x.projid == id).FirstOrDefault();
           
            return View(new EditProject
            {
                pid = getdata.projid,
                description = getdata.description,
                budget = getdata.budget,
                duration = getdata.duration,
                deadline = getdata.deadline,

            });
        }
        [HttpPost]
        public ActionResult editProject(EditProject proj)
        {
            if (ModelState.IsValid)
            {
                db.sp_editProject(proj.pid, proj.description, proj.budget, proj.duration, proj.deadline,"active");
                proj.msg = "Project updated";
                return RedirectToAction("Index", proj);
            }

            return View();
        }

        public ActionResult viewBids(int id)
        {
            var bidlist = db.sp_GetProposalsByProjectId(id).ToList();
            if (bidlist.Any())
            {
                ViewBag.projectTitle = bidlist.First().title;
            }
            ViewBag.bids = bidlist;
            return View();
        }
    }
}