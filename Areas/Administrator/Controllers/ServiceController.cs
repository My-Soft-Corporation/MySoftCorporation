using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MySoftCorporation.Areas.Administrator.Controllers
{
    [Authorize(Roles = "General Manager,Administrator")]
    public class ServiceController : Controller
    {
        // GET: Administrator/Service
        public ActionResult Index()
        {
            return View();
        }
    }
}