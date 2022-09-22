using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BaseSystem.Controllers
{
    public class MobilePageController : Controller
    {
        // GET: MobilePage
        public ActionResult Index()
        {
            return View("Login");
        }
        public ActionResult Menu()
        {
            return PartialView("Menu");
        }
        public ActionResult SignForm()
        {
            return PartialView("SignForm");
        }
        public ActionResult AttRec()
        {
            return PartialView("AttRec");
        }
    }
}