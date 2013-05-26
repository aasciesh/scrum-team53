using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ScrumMainApp.Controllers
{
    public class HTTPstatusController : Controller
    {
        //
        // GET: /HTTPstatus/

        public ViewResult error404()
        {
            return View();
        }
        public ViewResult error403()
        {
            return View();
        }
    }
}
