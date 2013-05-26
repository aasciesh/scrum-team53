using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ScrumMainApp.Models;

namespace ScrumMainApp.Controllers
{ 
    public class SessionController : Controller
    {
        private ScrumDBEntities2 db = new ScrumDBEntities2();

        


        public ActionResult LogIn()
        {
            if (Session["userId"] != null)
                return RedirectToAction("Index", "Project");


            return View();
        } 

        [HttpPost]
        public ActionResult LogIn(User user)
        {
            try
            {
                
                if (ModelState.IsValid )
                {
                    
                    var userdb = db.Users.Include("companyRole").Single(b => b.userName == user.userName);

                    if (userdb.password == user.password)
                    {
                        Session["UserId"] = userdb.userId;
                        Session["UserName"] = userdb.userName;
                        if (userdb.CompanyRole.companyRole1 == "administrator")
                        {
                            return RedirectToAction("Index", "Admin");
                        }

                        Session["isScrumParticipant"] = "yes";
                        return RedirectToAction("Index", "Project");
                    }
                    else {
                        ViewBag.errorMsg = "Looks like either username or password or both is incorrect.";
                        return View();
                    }
                }

                return View();
            }

            catch
            {
                ViewBag.errorMsg = "Looks like either username or password or both is incorrect.";
                return View();
            }
        }
        public ActionResult Logout()
        {

            Session.Remove("UserId");
            Session.Remove("UserName");
            Session.Remove("Role");
            Session.Remove("projectId");
            Session.Remove("isScrumParticipant");
            return RedirectToAction("LogIn", "Session");
        }
       

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}