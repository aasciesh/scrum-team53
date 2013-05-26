using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcApplication1.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (Session["userId"] != null)
            {
                if (Session["isScrumParticipant"] != null)
                {
                    return RedirectToAction("Index", "Project");
                }
                else
                    return RedirectToAction("Index","Admin");
            
            }
            return View();
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
