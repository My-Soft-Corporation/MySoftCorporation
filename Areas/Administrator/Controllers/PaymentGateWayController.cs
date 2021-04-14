using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySoftCorporation.Helpers;
using MySoft.Payment.Entities.Models;
using System.Web.Mvc;
using MySoftCorporation.Services.Payments;
using System.Threading.Tasks;
namespace MySoftCorporation.Areas.Administrator.Controllers
{
    public class PaymentGateWayController : Controller
    {
        private readonly PaymentGateWayService _service;
        public PaymentGateWayController()
        {
            _service = new PaymentGateWayService();
        }
        // GET: Administrator/PaymentGateway
        public async Task<ActionResult> Index()
        {
            return View(await _service.Get());
        }

        [HttpGet]
        public async Task<ActionResult> Action(int ID = 0)
        {
            if (ID == 0)
            {
                PaymentGateway paymentGateway = new PaymentGateway
                {
                    IP = UserInfo.IP(),
                    Agent = UserInfo.Agent(),
                    Location = "N/A",
                    UserID = UserHelperInfo.GetUserId()
                };
                return View("_Action", paymentGateway);
            }
            else
            {
                return View("_Action",await _service.GetById(ID));
            }
        }

        //[HttpGet]
        //public ActionResult Delete(int ID)
        //{
        //    if (Msg == "Success")
        //    {

        //        return PartialView("_Delete", model);
        //    }
        //    else
        //    {
        //        return PartialView("_Delete", new PaymentGatewayModel());
        //    }
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(PaymentGatewayModel model)
        //{
        //    (bool IsTrue, string Msg) = service.Delete(model.ID);
        //    JsonResult jsonResult = new JsonResult
        //    {
        //        Data = new { Success = IsTrue, ResponseMsg = Msg }
        //    };
        //    return jsonResult;
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Action(PaymentGateway paymentGateway)
        {
            paymentGateway.ModifiedOn = DateTimeHelper.Now();

           (bool IsTrue, string Msg) =await _service.Save(paymentGateway);
            if (IsTrue)
            {
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("Error", Msg);
                return View("_Action",paymentGateway);
            }
        }
    }
}