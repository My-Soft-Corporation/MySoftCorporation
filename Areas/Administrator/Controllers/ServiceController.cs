using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MySoftCorporation.Areas.Administrator.Controllers
{
    public class ServiceController : AdminAuthorizeController
    {
        // GET: Administrator/Service
        public ActionResult Index()
        {
            return View();
        }
    }
}