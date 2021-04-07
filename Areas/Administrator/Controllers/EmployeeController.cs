using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using MySoft.Employee.Entities;
using MySoft.Employee.Entities.Helpers;
using MySoftCorporation.Areas.Administrator.Models;
using MySoftCorporation.Helpers;
using MySoftCorporation.Services;
using System;
using System.Data.Entity.Validation;
using System.Web;
using System.Web.Mvc;

namespace MySoftCorporation.Areas.Administrator.Controllers
{
    [Authorize(Roles = "General Manager,Administrator")]
    public class EmployeeController : Controller
    {
        private readonly EmployeeService service = new EmployeeService();
        private SignManagerService _signInManager;
        private UserManagerService _userManager;

        public SignManagerService SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<SignManagerService>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public UserManagerService UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<UserManagerService>();
            }
            private set
            {
                _userManager = value;
            }
        }

        // GET: Administrator/Employee
        public ActionResult Index(string SearchTerm)
        {
            EmployeeViewModel employeeViewModel = new EmployeeViewModel
            {
                All = service.Search(SearchTerm),
                SearchTerm = SearchTerm
            };
            return View(employeeViewModel);
        }

        [HttpGet]
        public ActionResult Action(int? ID)
        {
            EmployeeActionModel model = new EmployeeActionModel();
            if (ID.HasValue)
            {
                Employee employee = service.GetByID(ID.Value);
                model.FirstName = employee.FirstName;
                model.LastName = employee.LastName;
                model.CNIC = employee.CNIC;
                model.DateOfBirth = employee.DateOfBirth;
                model.BloodGroup = employee.BloodGroup;
                model.Contact = employee.Contact;
                model.PresentAddress = employee.PresentAddress;
                model.PermenantAddress = employee.PermenantAddress;
                model.Gender = employee.Gender;
                model.Email = employee.Email;
                model.CityID = employee.CityID;
                model.UserID = employee.UserID;
                model.ID = employee.ID;
            }
            CityService cityService = new CityService();
            model.Cities = cityService.GetAll();
            model.Users = UserManager.Users;
            return PartialView("_Action", model);
        }

        [HttpGet]
        public ActionResult Delete(int ID)
        {
            Employee employee = service.GetByID(ID);
            EmployeeActionModel employeeActionModel = new EmployeeActionModel
            {
                ID = employee.ID,
                FirstName = employee.FirstName
            };
            return PartialView("_Delete", employeeActionModel);
        }

        [HttpPost]
        public ActionResult Delete(EmployeeActionModel employeeActionModel)
        {
            Employee employee = service.GetByID(employeeActionModel.ID);
            bool result;
            string msg = "";
            try
            {
                result = service.Delete(employee);
            }
            catch (Exception exc)
            {
                msg += exc.Message.ToString();
                result = false;
            }

            JsonResult jsonResult = new JsonResult
            {
                Data = result ? (new { Success = true, Msg = msg }) : (new { Success = false, Msg = msg }),
            };
            return jsonResult;
        }

        [HttpPost]
        public JsonResult Action(EmployeeActionModel model)
        {
            bool result;
            string msg = "";
            if (model.ID > 0)
            {
                var (isTrue, Msg) = service.Update(model);
                msg = Msg;
                result = isTrue;
            }
            else
            {
                Employee employee = new Employee
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    CNIC = model.CNIC,
                    DateOfBirth = model.DateOfBirth,
                    BloodGroup = model.BloodGroup,
                    Contact = model.Contact,
                    PresentAddress = model.PresentAddress,
                    PermenantAddress = model.PermenantAddress,
                    Gender = model.Gender,
                    ModifiedOn = DateTime.Now,
                    IP = UserInfo.IP(),
                    Agent = UserInfo.Agent(),
                    Location = UserInfo.Location(),
                    Email = model.Email,
                    UserID = User.Identity.GetUserId(),
                    CityID = model.CityID
                };
                try
                {
                    result = service.Save(employee).isSaved;
                }
                catch (DbEntityValidationException exc)
                {
                    foreach (var eve in exc.EntityValidationErrors)
                    {
                        foreach (var ve in eve.ValidationErrors)
                        {
                            msg += ve.ErrorMessage + " Property Type : " + ve.PropertyName;
                        }
                    }
                    result = false;
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