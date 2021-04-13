using MySoft.Institute.Entities;
using MySoftCorporation.Areas.Administrator.Models;
using MySoftCorporation.Helpers;
using MySoftCorporation.Services;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MySoftCorporation.Areas.Administrator.Controllers
{
    public class AdmissionController : AdminAuthorizeController
    {
        private readonly AdmissionService service;
        public AdmissionController()
        {
            service = new AdmissionService();
        }
       
        public async Task<ActionResult> Index()
        {
            AdmissionViewModel admissionViewModel = new AdmissionViewModel
            {
                Admissions = await service.GetAdmissons()
            };
            return View(admissionViewModel);
        }

        [HttpPost]
        public JsonResult ApproveAdmission(AdmissionActionModel admissionActionModel)
        {
            JsonResult jsonResult = new JsonResult();
            var OldAdmission = service.GetByID(admissionActionModel.AdmissionID);
            if (OldAdmission != null)
            {
                Admission admission = new Admission()
                {
                    AdmissionID = OldAdmission.AdmissionID,
                    StudentID = OldAdmission.StudentID,
                    CourseID = OldAdmission.CourseID,
                    IsConfirmed = true,
                    UserID = UserHelperInfo.GetUserId(),
                    ModifiedOn = DateTime.Now,
                    IP = UserInfo.IP(),
                    Agent = UserInfo.Agent(),
                    Location = UserInfo.Location()
                };
                var (IsTrue, Msg) = service.ApproveAdmission(admission);
                jsonResult.Data = new { Success = IsTrue, Message = Msg };
            }
            else
            {
                jsonResult.Data = new { Success = false, Message = "No Admission Found In DB" };
            }

            return jsonResult;
        }
    }
}