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
    public class projectController : Controller
    {
        private ScrumDBEntities2 db = new ScrumDBEntities2();

        //
        // GET: /project/
        public ViewResult Dashboard(int id)
        {
            try
            {
                if (Session["userId"] == null)
                    return View("~/Views/HTTPstatus/error403.cshtml");
               

                Session["projectID"] = id;
                 
                var userId = Convert.ToInt32(Session["userId"]);
                var roleRow = db.Participations.Single(p => p.projectId == id && p.userId == userId);
                Session["role"] = roleRow.role;
                Project project = db.Projects.Single(p => p.projectId == id);
                Session["projectName"] = project.projectName;
                return View(project);
            }
            catch(InvalidOperationException)
            {
                Session["projectID"] = null;
                return View("~/Views/HTTPstatus/error403.cshtml");
            
            }
        }

        public PartialViewResult allProjectList()
        {
            return PartialView(db.Projects.ToList());
        }

        public PartialViewResult projectlist()
        {
            List<Project> projList = new List<Project>();
            int userId = Convert.ToInt32(Session["userId"]);
            var dbb = db.Participations.Where(i => i.userId == userId).ToList();
            var projects = db.Projects.ToList();

            foreach (var item in dbb)
            {
                foreach (var projs in projects)
                {
                    if (item.projectId == projs.projectId)
                        projList.Add(projs);

                }
            }
            return PartialView("_projectList", projList);

        }

        public ViewResult Index()
        {
            
            List<Project> projList = new List<Project>();
            int userId = Convert.ToInt32(Session["userId"]);
            var dbb = db.Participations.Where(i => i.userId == userId).ToList();
            var projects = db.Projects.ToList();

            foreach (var item in dbb)
            {
                foreach (var projs in projects)
                {
                    if (item.projectId == projs.projectId)
                        projList.Add(projs);

                }
            }
            return View(projList);
        }

        //
        // GET: /project/Details/5

        public ViewResult Details(int id)
        {
            Session["projectID"] = id;
            Project project = db.Projects.Single(p => p.projectId == id);
            return View(project);
        }

        //
        // GET: /project/Create

        public ActionResult Create()
        {
           
                return View();
            
        }

        //
        // POST: /project/Create

        [HttpPost]
        public ActionResult Create(Project project)
        {
             if (ModelState.IsValid)
                {
                    if (Session["UserId"] == null)
                        return RedirectToAction("error403", "HTTPstatus");

                    db.Projects.AddObject(project);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

             return View(project);
        }

        //
        // GET: /project/Edit/5

        public ActionResult Edit(int id)
        {
            Project project = db.Projects.Single(p => p.projectId == id);
            return View(project);
        }

        //
        // POST: /project/Edit/5

        [HttpPost]
        public ActionResult Edit(Project project)
        {
            if (ModelState.IsValid)
            {
                db.Projects.Attach(project);
                db.ObjectStateManager.ChangeObjectState(project, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(project);
        }

        //
        // GET: /project/Delete/5

        public ActionResult Delete(int id)
        {
            Project project = db.Projects.Single(p => p.projectId == id);
            return View(project);
        }

        //
        // POST: /project/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Project project = db.Projects.Single(p => p.projectId == id);
            db.Projects.DeleteObject(project);
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