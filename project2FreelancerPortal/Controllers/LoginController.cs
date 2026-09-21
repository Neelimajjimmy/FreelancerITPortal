using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using project2FreelancerPortal.Models;

namespace project2FreelancerPortal.Controllers
{
    public class LoginController : Controller
    {
        freelanceDbEntities db = new freelanceDbEntities();
        // GET: Login
        public ActionResult Login_load()
        {
            return View();
        }

        public ActionResult login_click(Login ob)
        {
            if (ModelState.IsValid)
            {
                var count = db.sp_LoginDb(ob.username, ob.pass).First();
                if (count == 1)
                {
                    var getid = db.sp_GetId(ob.username, ob.pass).FirstOrDefault();
                    Session["userid"] = getid;
                    var logtype = db.sp_GetLogtype(ob.username, ob.pass).FirstOrDefault();

                    if (logtype == "admin")
                    {
                        return RedirectToAction("Index", "Admin");
                    }
                    else
                    {
                        return RedirectToAction("userIndex", "User");
                    }
                }

            }
            return View("Login_load");
        }
    }
}