using MySoft.Account.Entities;
using System.Web.Mvc;

namespace MySoftCorporation.Areas.Administrator.Controllers
{
    [Authorize(Roles = "General Manager,Administrator")]
    public class VoucherController : Controller
    {
        // GET: Administrator/Voucher
        public ActionResult Index()
        {
            Voucher voucher = new Voucher();
            return View(voucher);
        }
    }
}