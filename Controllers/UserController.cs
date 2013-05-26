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
    public class UserController : Controller
    {
        private ScrumDBEntities2 db = new ScrumDBEntities2();

        //
        // GET: /User/

        public ViewResult Index()
        {
            var users = db.Users.Include("CompanyRole");
            return View(users.ToList());
        }

        public PartialViewResult allUsersList()
        {
            return PartialView(db.Users.ToList());
        }
        // GET: /User/Details/5

        public ViewResult Details(int id)
        {
            User user = db.Users.Single(u => u.userId == id);
            return View(user);
        }

        //
        // GET: /User/Create

        public ActionResult Create()
        {
            ViewBag.companyRoleId = new SelectList(db.CompanyRoles, "companyRoleId", "companyRole1");
            return View();
        } 

        //
        // POST: /User/Create

        [HttpPost]
        public ActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                db.Users.AddObject(user);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.companyRoleId = new SelectList(db.CompanyRoles, "companyRoleId", "companyRole1", user.companyRoleId);
            return View(user);
        }
        
        //
        // GET: /User/Edit/5
 
        public ActionResult Edit(int id)
        {
            User user = db.Users.Single(u => u.userId == id);
            ViewBag.companyRoleId = new SelectList(db.CompanyRoles, "companyRoleId", "companyRole1", user.companyRoleId);
            return View(user);
        }

        //
        // POST: /User/Edit/5

        [HttpPost]
        public ActionResult Edit(User user)
        {
            if (ModelState.IsValid)
            {
                db.Users.Attach(user);
                db.ObjectStateManager.ChangeObjectState(user, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.companyRoleId = new SelectList(db.CompanyRoles, "companyRoleId", "companyRole1", user.companyRoleId);
            return View(user);
        }

        //
        // GET: /User/Delete/5
 
        public ActionResult Delete(int id)
        {
            User user = db.Users.Single(u => u.userId == id);
            return View(user);
        }

        //
        // POST: /User/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            User user = db.Users.Single(u => u.userId == id);
            db.Users.DeleteObject(user);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}