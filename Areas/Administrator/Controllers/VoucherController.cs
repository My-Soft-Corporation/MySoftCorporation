using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MySoftCorporation.Areas.Administrator.Controllers
{
    [Authorize(Roles = "General Manager,Administrator")]
    public class VoucherController : Controller
    {
        // GET: Administrator/Voucher
        public ActionResult Index()
        {
            return View();
        }
    }
}