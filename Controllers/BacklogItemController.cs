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
    public class BacklogItemController : Controller
    {
        private ScrumDBEntities2 db = new ScrumDBEntities2();

        //
        // GET: /BacklogItem/
        public PartialViewResult backlogItemsList(int id)
        {

            if (Session["userId"] == null)
                return PartialView("~/Views/HTTPstatus/error403.cshtml");

            var backlogitems = db.BacklogItems.Include(i => i.Project).Include("Sprint").Include("Status1").Where(i => i.projectId == id && i.sprintId== null).OrderBy(i => i.priority); ;

            if (Request.IsAjaxRequest())
                 return PartialView("_backlogItemList", backlogitems.ToList());
            
            return PartialView("_backlogItemList", backlogitems.ToList());
        }
        public PartialViewResult allBacklogItemsList(int id)
        {

            if (Session["userId"] == null)
                return PartialView("~/Views/HTTPstatus/error403.cshtml");

            var backlogitems = db.BacklogItems.Include(i => i.Project).Include("Sprint").Include("Status1").Where(i => i.projectId == id).OrderBy(i => i.priority); ;

            if (Request.IsAjaxRequest())
                return PartialView("_backlogItemList", backlogitems.ToList());

            return PartialView("_backlogItemList", backlogitems.ToList());
        }
        public PartialViewResult sprintBacklogItemsList(int id)
        {
            
                if (Session["userId"] == null)
                    return PartialView("~/Views/HTTPstatus/error403.cshtml");

                var sprintBacklogItems = db.BacklogItems.Include(i => i.Project).Include("Sprint").Include("Status1").Where(i => i.sprintId == id).OrderBy(i => i.priority);
                if (Request.IsAjaxRequest())
                    return PartialView("_sprintBacklog", sprintBacklogItems.ToList());
                return PartialView("_sprintBacklog", sprintBacklogItems.ToList());
            
        }
        public ViewResult Index()
        {
            
                if (Session["userId"] == null)
                    return View("~/Views/HTTPstatus/error403.cshtml");

                var backlogitems = db.BacklogItems.Include("Project").Include("Sprint").Include("Status1");
                return View(backlogitems.ToList());
           
        }

        
        //
        // GET: /BacklogItem/Details/5

        public ViewResult Details(int id)
        {
            
                if (Session["userId"] == null)
                    return View("~/Views/HTTPstatus/error403.cshtml");

            BacklogItem backlogitem = db.BacklogItems.Single(b => b.backlogId == id);
            if (Request.IsAjaxRequest())
                return View("_Details", backlogitem);
            return View(backlogitem);
            
        }

        //
        // GET: /BacklogItem/Create

        public ActionResult Create()
        {
            if (Session["userId"] == null)
                return RedirectToAction("login", "Session");
            else if (Convert.ToString(Session["role"]) != "product owner")
                return RedirectToAction("error403", "HTTPstatus");

            int projectId= Convert.ToInt32(Session["projectId"]);
            ViewBag.projectId = new SelectList((db.Projects.Where(p => p.projectId == projectId)), "projectId", "projectName");
            ViewBag.sprintId = new SelectList(db.Sprints.Where(i => i.projectId== projectId ), "sprintId", "sprintNumber");
            ViewBag.status = new SelectList(db.Status, "status1", "status1");
            return View();
        } 

        //
        // POST: /BacklogItem/Create

        [HttpPost]
        public ActionResult Create(BacklogItem backlogitem)
        {
            if (Session["userId"] == null)
                return RedirectToAction("login", "Session");
            else if (Convert.ToString(Session["role"]) != "product owner")
                return RedirectToAction("error403", "HTTPstatus");

            if (ModelState.IsValid)
            {
                db.BacklogItems.AddObject(backlogitem);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            int projectId = Convert.ToInt32(Session["projectId"]);
            ViewBag.projectId = new SelectList((db.Projects.Where(p => p.projectId == projectId)), "projectId", "projectName");
            ViewBag.sprintId = new SelectList(db.Sprints.Where(i => i.projectId == projectId), "sprintId", "sprintNumber");
            ViewBag.status = new SelectList(db.Status, "status1", "status1", backlogitem.status);
            return View(backlogitem);
        }
        
        //
        // GET: /BacklogItem/Edit/5
 
        public ActionResult Edit(int id)
        {
            if (Session["userId"] == null)
                return RedirectToAction("login", "Session");
            else if (Convert.ToString(Session["role"]) != "product owner")
                return RedirectToAction("error403", "HTTPstatus");

            BacklogItem backlogitem = db.BacklogItems.Single(b => b.backlogId == id);
            ViewBag.projectId = new SelectList(db.Projects, "projectId", "projectName", backlogitem.projectId);
            ViewBag.sprintId = new SelectList(db.Sprints, "sprintId", "sprintNumber", backlogitem.sprintId);
            ViewBag.status = new SelectList(db.Status, "status1", "status1", backlogitem.status);
            return View(backlogitem);
        }

        public ActionResult backlogItemToSprint(string fromType, int fromId, string toType, int toId, int backlogId)
        {

            //if (Request.IsAjaxRequest())
            //    return Json(new { msg = "success" }, JsonRequestBehavior.AllowGet);
            //if()
            //{
            
            //}
            try
            {
                if (Session["userId"] == null)
                    return RedirectToAction("login", "Session");
                else if (Convert.ToString(Session["role"]) != "product owner")
                    return Json(new { message = "error" }, JsonRequestBehavior.AllowGet);

                if (fromType == "backlog")
                {
                    if (toType == "sprint")
                    {
                        BacklogItem backlogitem = db.BacklogItems.Single(b => b.backlogId == backlogId);
                        backlogitem.sprintId = toId;
                        db.BacklogItems.Attach(backlogitem);
                        db.ObjectStateManager.ChangeObjectState(backlogitem, EntityState.Modified);
                        db.SaveChanges();
                    }

                }
                else if (fromType == "sprint")
                {
                    if (toType == "sprint")
                    {
                        BacklogItem backlogitem = db.BacklogItems.Single(b => b.backlogId == backlogId);
                        backlogitem.sprintId = toId;
                        db.BacklogItems.Attach(backlogitem);
                        db.ObjectStateManager.ChangeObjectState(backlogitem, EntityState.Modified);
                        db.SaveChanges();
                    }
                    if (toType == "backlog")
                    {
                        BacklogItem backlogitem = db.BacklogItems.Single(b => b.backlogId == backlogId);
                        backlogitem.sprintId = null;
                        db.BacklogItems.Attach(backlogitem);
                        db.ObjectStateManager.ChangeObjectState(backlogitem, EntityState.Modified);
                        db.SaveChanges();
                    }
                }
                return Json(new { message = "success" }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new { message = "error" }, JsonRequestBehavior.AllowGet);
            
            }
        }
        //
        // POST: /BacklogItem/Edit/5

        [HttpPost]
        public ActionResult Edit(BacklogItem backlogitem)
        {
            if (Session["userId"] == null)
                return RedirectToAction("login", "Session");
            else if (Convert.ToString(Session["role"]) != "product owner")
                return RedirectToAction("error403", "HTTPstatus");

            if (ModelState.IsValid)
            {
                db.BacklogItems.Attach(backlogitem);
                db.ObjectStateManager.ChangeObjectState(backlogitem, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.projectId = new SelectList(db.Projects, "projectId", "projectName", backlogitem.projectId);
            ViewBag.sprintId = new SelectList(db.Sprints, "sprintId", "sprintNumber", backlogitem.sprintId);
            ViewBag.status = new SelectList(db.Status, "status1", "status1", backlogitem.status);
            return View(backlogitem);
        }

        //
        // GET: /BacklogItem/Delete/5
 
        public ActionResult Delete(int id)
        {
            if (Session["userId"] == null)
                return RedirectToAction("login", "Session");
            else if (Convert.ToString(Session["role"]) != "product owner")
                return RedirectToAction("error403", "HTTPstatus");

            BacklogItem backlogitem = db.BacklogItems.Single(b => b.backlogId == id);
            return View(backlogitem);
        }

        //
        // POST: /BacklogItem/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Session["userId"] == null)
                return RedirectToAction("login", "Session");
            else if (Convert.ToString(Session["role"]) != "product owner")
                return RedirectToAction("error403", "HTTPstatus");
 
            BacklogItem backlogitem = db.BacklogItems.Single(b => b.backlogId == id);
            db.BacklogItems.DeleteObject(backlogitem);
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