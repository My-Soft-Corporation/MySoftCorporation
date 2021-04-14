using MySoftCorporation.Services.Payments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MySoftCorporation.Areas.Administrator.Controllers
{
    [RouteArea("Administrator")]
    public class StudentPaymentController : AdminAuthorizeController
    {
        private readonly FeePaymentService _feePaymentService;
        public StudentPaymentController()
        {
            _feePaymentService = new FeePaymentService();
        }
        public async Task<ActionResult> Index()
        {
            return View(await _feePaymentService.Get());
        }
    }
}