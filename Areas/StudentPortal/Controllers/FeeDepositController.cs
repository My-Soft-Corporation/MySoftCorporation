using MySoft.Institute.Entities.Accounts;
using MySoftCorporation.Areas.StudentPortal.Models;
using MySoftCorporation.Helpers;
using MySoftCorporation.Services;
using MySoftCorporation.Services.Payments;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using static System.Net.WebRequestMethods;

namespace MySoftCorporation.Areas.StudentPortal.Controllers
{
    public class FeeDepositController : StudentAuthorizeController
    {
        private readonly PaymentGateWayService _paymentGateWayService;
        private readonly FeePaymentService _feePaymentService;
        private readonly AdmissionService _admissionService;
        private readonly StudentService _studentService;
        public FeeDepositController()
        {
            _feePaymentService = new FeePaymentService();
            _paymentGateWayService = new PaymentGateWayService();
            _admissionService = new AdmissionService();
            _studentService = new StudentService();
        }
        [Authorize]
        public async Task<ActionResult> Index()
        {
            string UserId = UserHelperInfo.GetUserId();
            var selectedStudent = _studentService.GetStudentByUserId(UserId);
            if (selectedStudent == null)
            {
                return HttpNotFound("Student Not Found In DB");
            }
            var LastAdmission = await _admissionService.GetLastAdmission(selectedStudent.ID);
            StudentPaymentModel model = new StudentPaymentModel()
            {
                FeePayments = await _feePaymentService.GetByAdmissionId(LastAdmission.AdmissionID),
                PaymentGateways = await _paymentGateWayService.Get()
            };
            return View(model);
        }
        [HttpGet]
        [Authorize]
        public async Task<ActionResult> Confirm(int id = 0)
        {
            var selectedGateway =  await _paymentGateWayService.GetById(id);
            string UserId = UserHelperInfo.GetUserId();
            var selectedStudent =  _studentService.GetStudentByUserId(UserId);
            if (selectedStudent == null)
            {
                return  HttpNotFound("Student Not Found In DB");
            }
            var LastAdmission = await _admissionService.GetLastAdmission(selectedStudent.ID);
            FeePayment feePayment = new FeePayment
            {
                PaymentGateway = selectedGateway,
                PaymentGatewayId = selectedGateway.ID,
                Id = 0,
                AdmissionId = LastAdmission.AdmissionID,
                IP = UserInfo.IP(),
                Agent = UserInfo.Agent()
            };
            return View(feePayment);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Confirm(FeePayment feePayment)
        {
                feePayment.ModifiedOn = DateTimeHelper.Now();
                var (IsTrue, ResponseMsg) = await _feePaymentService.Save(feePayment);
                if (IsTrue)
                    return RedirectToAction("Index");
                else
                {
                    ModelState.AddModelError("Error", ResponseMsg);
                    return View(feePayment);
                }
        }
    }
}