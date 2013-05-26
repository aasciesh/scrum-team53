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
    public class SprintController : Controller
    {
        private ScrumDBEntities2 db = new ScrumDBEntities2();

        //
        // GET: /Sprint/
        public PartialViewResult SprintsList(int id)
        {
            var sprints = db.Sprints.Include("Project").Where(i => i.projectId == id).OrderBy(i => i.sprintNumber);
            return PartialView("_sprintsList", sprints.ToList());

        }
        public PartialViewResult SprintsListfortask(int id)
        {
            var sprints = db.Sprints.Include("Project").Where(i => i.projectId == id).OrderBy(i => i.sprintNumber);
            return PartialView("_sprintsListForTask", sprints.ToList());

        }
        public PartialViewResult SprintsListForCharts(int id)
        {
            var sprints = db.Sprints.Include("Project").Where(i => i.projectId == id).OrderBy(i => i.sprintNumber);
            return PartialView(sprints.ToList());

        }
        public ViewResult Index()
        {
            var sprints = db.Sprints.Include("Project");
            return View(sprints.ToList());
        }

        //
        // GET: /Sprint/Details/5

        public ViewResult Details(int id)
        {
            Sprint sprint = db.Sprints.Single(s => s.sprintId == id);
            return View(sprint);
        }

        //
        // GET: /Sprint/Create

        public ActionResult Create()
        {
            if(Session["userId"] == null)
                return RedirectToAction("Login","session");

            else if (Convert.ToString(Session["role"]) != "product owner")
                  return RedirectToAction("Dashboard", "Praject", new { id = Convert.ToInt32(Session["projectId"])});

            int projectId = Convert.ToInt32(Session["projectId"]);
            ViewBag.projectId = new SelectList(db.Projects.Where(i => i.projectId == projectId), "projectId", "projectName");
            return View();
        } 

        //
        // POST: /Sprint/Create

        [HttpPost]
        public ActionResult Create(Sprint sprint)
        {
            if (Session["userId"] == null)
                return RedirectToAction("Login", "session");

            else if (Convert.ToString(Session["role"]) != "product owner")
                return RedirectToAction("Dashboard", "Praject", new { id = Convert.ToInt32(Session["projectId"]) });

            if (ModelState.IsValid)
            {
                db.Sprints.AddObject(sprint);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            int projectId = Convert.ToInt32(Session["projectId"]);
            ViewBag.projectId = new SelectList(db.Projects.Where(i => i.projectId == projectId), "projectId", "projectName");
            return View(sprint);
        }
        
        //
        // GET: /Sprint/Edit/5
 
        public ActionResult Edit(int id)
        {
            Sprint sprint = db.Sprints.Single(s => s.sprintId == id);
            ViewBag.projectId = new SelectList(db.Projects, "projectId", "projectName", sprint.projectId);
            return View(sprint);
        }

        //
        // POST: /Sprint/Edit/5

        [HttpPost]
        public ActionResult Edit(Sprint sprint)
        {
            if (ModelState.IsValid)
            {
                db.Sprints.Attach(sprint);
                db.ObjectStateManager.ChangeObjectState(sprint, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.projectId = new SelectList(db.Projects, "projectId", "projectName", sprint.projectId);
            return View(sprint);
        }

        //
        // GET: /Sprint/Delete/5
 
        public ActionResult Delete(int id)
        {
            Sprint sprint = db.Sprints.Single(s => s.sprintId == id);
            return View(sprint);
        }

        //
        // POST: /Sprint/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            Sprint sprint = db.Sprints.Single(s => s.sprintId == id);
            db.Sprints.DeleteObject(sprint);
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