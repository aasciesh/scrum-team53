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
    public class ParticipationController : Controller
    {
        private ScrumDBEntities2 db = new ScrumDBEntities2();

        //
        // GET: /Participation/

        public ViewResult Index()
        {
            var participations = db.Participations.Include("Project").Include("Role1").Include("User");
            return View(participations.ToList());
        }

        //
        // GET: /Participation/Details/5
        public PartialViewResult participationInProject(int id)
        {
            var participations = db.Participations.Include("Project").Include("Role1").Include("User").Where(p => p.projectId== id);
            return PartialView(participations);
        
        }
        public ViewResult Details(int id)
        {
            Participation participation = db.Participations.Single(p => p.projectId == id);
            return View(participation);
        }

        //
        // GET: /Participation/Create

        public ActionResult Create()
        {
            ViewBag.projectId = new SelectList(db.Projects, "projectId", "projectName");
            ViewBag.role = new SelectList(db.Roles, "role1", "role1");
            ViewBag.userId = new SelectList(db.Users, "userId", "firstName");
            return View();
        } 

        //
        // POST: /Participation/Create

        [HttpPost]
        public ActionResult Create(Participation participation)
        {
            if (ModelState.IsValid)
            {
                db.Participations.AddObject(participation);
                db.SaveChanges();
                return RedirectToAction("Details", "Project", participation.projectId);  
            }

            ViewBag.projectId = new SelectList(db.Projects, "projectId", "projectName", participation.projectId);
            ViewBag.role = new SelectList(db.Roles, "role1", "role1", participation.role);
            ViewBag.userId = new SelectList(db.Users, "userId", "firstName", participation.userId);
            return View(participation);
        }
        
        //
        // GET: /Participation/Edit/5
 
        public ActionResult Edit(int projId, int userId)
        {
            Participation participation = db.Participations.Single(p => p.projectId == projId && p.userId == userId);
            Session["oldModel"] = participation;
            ViewBag.role = new SelectList(db.Roles, "role1", "role1", participation.role);
            ViewBag.userId = new SelectList(db.Users, "userId", "firstName", participation.userId);
            return View(participation);
        }

        //
        // POST: /Participation/Edit/5

        [HttpPost]
        public ActionResult Edit(Participation participation)
        {
            if (ModelState.IsValid)
            {
                Participation oldParticipation = (Participation)Session["oldModel"];
                db.Participations.Attach(oldParticipation);

                oldParticipation.projectId = participation.projectId;
                oldParticipation.userId = participation.userId;
                oldParticipation.role = participation.role;
                db.ObjectStateManager.ChangeObjectState(oldParticipation, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Details","Project", participation.projectId);
            }
            ViewBag.projectId = new SelectList(db.Projects, "projectId", "projectName", participation.projectId);
            ViewBag.role = new SelectList(db.Roles, "role1", "role1", participation.role);
            ViewBag.userId = new SelectList(db.Users, "userId", "firstName", participation.userId);
            return View(participation);
        }

        //
        // GET: /Participation/Delete/5

        public ActionResult Delete(int projId, int userId)
        {
            Participation participation = db.Participations.Single(p => p.projectId == projId && p.userId == userId);
            return View(participation);
        }

        //
        // POST: /Participation/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int projId, int userId)
        {
            Participation participation = db.Participations.Single(p => p.projectId == projId && p.userId == userId);
            db.Participations.DeleteObject(participation);
            db.SaveChanges();
            return RedirectToAction("Details", "Project", participation.projectId);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}