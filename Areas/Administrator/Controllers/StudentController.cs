using MySoft.Institute.Entities;
using MySoftCorporation.Areas.Administrator.Models;
using MySoftCorporation.Helpers;
using MySoftCorporation.Services;
using System;
using System.Data.Entity.Validation;
using System.Web.Mvc;

namespace MySoftCorporation.Areas.Administrator.Controllers
{
    [Authorize(Roles = "General Manager,Administrator")]
    public class StudentController : Controller
    {
        private readonly BloodGroupService bloodGroupService = new BloodGroupService();
        private readonly CityService cityService = new CityService();

        // GET: Administrator/Student
        private readonly StudentService service = new StudentService();

        public ActionResult Index(string SearchTerm)
        {
            StudentViewModel model = new StudentViewModel
            {
                All = service.Search(SearchTerm),
                SearchTerm = SearchTerm
            };
            model.BloodGroups = bloodGroupService.GetAll();
            model.Cities = cityService.GetAll();
            return View(model);
        }

        [HttpGet]
        public ActionResult Action(int? ID)
        {
            StudentActionModel model = new StudentActionModel();
            if (ID.HasValue)
            {
                Student student = service.GetByID(ID.Value);
                model.ID = student.ID;
                model.FirstName = student.FirstName;
                model.LastName = student.LastName;
                model.CNIC = student.CNIC;
                model.FatherName = student.FatherName;
                model.FatherCNIC = student.FatherCNIC;
                model.FatherProfession = student.FatherProfession;
                model.StudentContact = student.StudentContact;
                model.FatherContact = student.FatherContact;
                model.EmergencyContact = student.EmergencyContact;
                model.DateOfBirth = student.DateOfBirth;
                model.BloodGroupID = student.BloodGroupID;
                model.Gender = student.Gender;
                model.MaritalStatus = student.MaritalStatus;
                model.CityID = student.CityID;
                model.PresentAddress = student.PresentAddress;
                model.PermenantAddress = student.PermenantAddress;
            }
            model.Cities = cityService.GetAll();
            model.BloodGroups = bloodGroupService.GetAll();
            return PartialView("_Action", model);
        }

        [HttpGet]
        public ActionResult Delete(int ID)
        {
            Student student = service.GetByID(ID);
            StudentActionModel model = new StudentActionModel
            {
                ID = student.ID,
                FirstName = student.FirstName
            };

            return PartialView("_Delete", model);
        }

        [HttpPost]
        public ActionResult Delete(StudentActionModel model)
        {
            Student objectData = service.GetByID(model.ID);
            string msg = "";
            bool result = false;
            try
            {
                result = service.Delete(objectData);
            }
            catch (Exception exc)
            {
                msg = exc.Message.ToString();
            }
            JsonResult jsonResult = new JsonResult
            {
                Data = result ? (new { Success = true, Msg = msg }) : (new { Success = false, Msg = msg }),
            };
            return jsonResult;
        }

        [HttpPost]
        public JsonResult Action(StudentActionModel model)
        {
            bool result;
            string msg = "";
            if (model.ID > 0)
            {
                Student objectFirst = service.GetByID(model.ID);
                try
                {
                    result = service.Update(objectFirst);
                }
                catch (Exception exc)
                {
                    result = false;
                    msg = exc.Message.ToString();
                }
            }
            else
            {
                Student objectFirst = new Student
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    CNIC = model.CNIC,
                    FatherName = model.FatherName,
                    FatherCNIC = model.FatherCNIC,
                    FatherProfession = model.FatherProfession,
                    StudentContact = model.StudentContact,
                    FatherContact = model.FatherContact,
                    EmergencyContact = model.EmergencyContact,
                    DateOfBirth = model.DateOfBirth,
                    BloodGroupID = model.BloodGroupID,
                    Gender = model.Gender,
                    MaritalStatus = model.MaritalStatus,
                    CityID = model.CityID,
                    PresentAddress = model.PresentAddress,
                    PermenantAddress = model.PermenantAddress,
                    IP = UserInfo.IP(),
                    Agent = UserInfo.Agent(),
                    Location = UserInfo.Location(),
                    ModifiedOn = DateTime.Now,
                    UserID = UserHelperInfo.GetUserId()
                };
                try
                {
                    result = service.Save(objectFirst);
                }
                catch (DbEntityValidationException exc)
                {
                    foreach (var validationException in exc.EntityValidationErrors)
                    {
                        foreach (var ve in validationException.ValidationErrors)
                        {
                            msg += ve.PropertyName + "" + ve.ErrorMessage;
                        }
                    }
                    result = false;
                }
                catch (Exception exc)
                {
                    result = false;
                    msg = exc.InnerException.ToString();
                }
            }

            JsonResult jsonResult = new JsonResult
            {
                Data = result ? (new { Success = true, Msg = msg }) : (new { Success = false, Msg = msg })
            };
            return jsonResult;
        }
    }
}