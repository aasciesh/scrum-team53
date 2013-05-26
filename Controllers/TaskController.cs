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
    public class TaskController : Controller
    {
        private ScrumDBEntities2 db = new ScrumDBEntities2();

        //
        // GET: /Task/

        public ViewResult Index()
        {
            
                if (Session["userId"] == null )
                    return View("~/Views/HTTPstatus/error403.cshtml");

                var tasks = db.Tasks.Include("BacklogItem").Include("Status1").Include("User");
                return View(tasks.ToList());
            
        }

        //
        // GET: /Task/Details/5
        public PartialViewResult allbacklogtask(int id)
        {
            
                if (Session["userId"] == null)
                    return PartialView("~/Views/HTTPstatus/error403.cshtml");

                List<Task> taskwithBacklog = new List<Task>();
                var TaskItems = db.Tasks.Where(i => i.backlogId == id).OrderBy(i => i.taskId).ToList();
                return PartialView(taskwithBacklog);
           


        }
        public PartialViewResult alltaskItemList(int id)
        {

            
                if (Session["userId"] == null)
                    return PartialView("~/Views/HTTPstatus/error403.cshtml");

                List<Task> taskWithProject = new List<Task>();
                var TaskItems = db.Tasks.OrderBy(i => i.taskId).ToList();
                var backlogItemlsit = db.BacklogItems.OrderBy(i => i.backlogId).ToList();
                foreach (var item in TaskItems)
                {
                    int bLId = item.backlogId;

                    foreach (var bLitem in backlogItemlsit)
                    {
                        if (bLitem.backlogId == item.backlogId)
                        {
                            if (bLitem.projectId == id)
                            {
                                taskWithProject.Add(item);
                            }
                        }

                    }
                }
                return PartialView(taskWithProject);
            
        }

        public PartialViewResult sprinttaskItemList(int id)
        {
            
                if (Session["userId"] == null)
                    return PartialView("~/Views/HTTPstatus/error403.cshtml");

                List<Task> taskWithSprint = new List<Task>();
                var sprintTaskItems = db.Tasks.Include("User").OrderBy(i => i.taskId).ToList();
                var backlogItemlsit = db.BacklogItems.OrderBy(i => i.backlogId).ToList();
                foreach (var item in sprintTaskItems)
                {
                    int bLId = item.backlogId;

                    foreach (var bLitem in backlogItemlsit)
                    {
                        if (bLitem.backlogId == item.backlogId)
                        {
                            if (bLitem.sprintId == id)
                            {
                                taskWithSprint.Add(item);
                            }
                        }

                    }


                }
                return PartialView("taskWithSprint", taskWithSprint);
            

        }
        public ViewResult Details(int id)
        {
            
                if (Session["userId"] == null)
                    throw new HttpException(403, "Forbidden");

                    Task task = db.Tasks.Single(t => t.taskId == id);
                    return View(task);
            
        }

        //
        // GET: /Task/Create
       
        public ActionResult Create()
        {
            
                if (Session["userId"] == null)
                    return RedirectToAction("login", "Session");
                else if (Convert.ToString(Session["role"]) != "developer")
                    return PartialView("~/Views/HTTPstatus/error403.cshtml");

                ViewBag.backlogId = new SelectList(db.BacklogItems, "backlogId", "story");
                ViewBag.status = new SelectList(db.Status, "status1", "status1");
                ViewBag.userId = new SelectList(db.Users, "userId", "firstName");
                return View();
            
            
        }

        public ActionResult CreateForThisBacklog(int id)
        {

            if (Session["userId"] == null)
                return RedirectToAction("login", "Session");
            else if (Convert.ToString(Session["role"]) != "developer")
                return PartialView("~/Views/HTTPstatus/error403.cshtml");

            ViewBag.backlogId = new SelectList(db.BacklogItems.Where(i => i.backlogId== id), "backlogId", "story");
            ViewBag.status = new SelectList(db.Status, "status1", "status1");
            ViewBag.userId = new SelectList(db.Users, "userId", "firstName");
            return View("Create");


        } 
        //
        // POST: /Task/Create

        [HttpPost]
        public ActionResult Create(Task task)
        {
            if (Session["userId"] == null)
                return RedirectToAction("login", "Session");
            else if (Convert.ToString(Session["role"]) != "developer")
                return PartialView("~/Views/HTTPstatus/error403.cshtml");

            if (ModelState.IsValid)
            {
                db.Tasks.AddObject(task);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.backlogId = new SelectList(db.BacklogItems, "backlogId", "story", task.backlogId);
            ViewBag.status = new SelectList(db.Status, "status1", "status1", task.status);
            ViewBag.userId = new SelectList(db.Users, "userId", "firstName", task.userId);
            return View(task);
        }
        
        //
        // GET: /Task/Edit/5
 
        public ActionResult Edit(int id)
        {
            if (Session["userId"] == null)
                return RedirectToAction("login", "Session");
            else if (Convert.ToString(Session["role"]) != "developer")
                return PartialView("~/Views/HTTPstatus/error403.cshtml");

            Task task = db.Tasks.Single(t => t.taskId == id);
            ViewBag.backlogId = new SelectList(db.BacklogItems, "backlogId", "story", task.backlogId);
            ViewBag.status = new SelectList(db.Status, "status1", "status1", task.status);
            ViewBag.userId = new SelectList(db.Users, "userId", "firstName", task.userId);
            return View(task);
        }

        //
        // POST: /Task/Edit/5

        [HttpPost]
        public ActionResult Edit(Task task)
        {
            if (Session["userId"] == null)
                return RedirectToAction("login", "Session");
            else if (Convert.ToString(Session["role"]) != "developer")
                return PartialView("~/Views/HTTPstatus/error403.cshtml");

            if (ModelState.IsValid)
            {
                db.Tasks.Attach(task);
                db.ObjectStateManager.ChangeObjectState(task, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.backlogId = new SelectList(db.BacklogItems, "backlogId", "story", task.backlogId);
            ViewBag.status = new SelectList(db.Status, "status1", "status1", task.status);
            ViewBag.userId = new SelectList(db.Users, "userId", "firstName", task.userId);
            return View(task);
        }

        //
        // GET: /Task/Delete/5
 
        public ActionResult Delete(int id)
        {
            if (Session["userId"] == null)
                return RedirectToAction("login", "Session");
            else if (Convert.ToString(Session["role"]) != "developer")
                return PartialView("~/Views/HTTPstatus/error403.cshtml");

            Task task = db.Tasks.Single(t => t.taskId == id);
            return View(task);
        }

        //
        // POST: /Task/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Session["userId"] == null)
                return RedirectToAction("login", "Session");
            else if (Convert.ToString(Session["role"]) != "developer")
                return PartialView("~/Views/HTTPstatus/error403.cshtml");

            Task task = db.Tasks.Single(t => t.taskId == id);
            db.Tasks.DeleteObject(task);
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