using MySoft.Employee.Entities;
using MySoftCorporation.Areas.Administrator.Models;
using MySoftCorporation.Helpers;
using MySoftCorporation.Services;
using System;
using System.Web.Mvc;

namespace MySoftCorporation.Areas.Administrator.Controllers
{
    [Authorize(Roles = "General Manager,Administrator")]
    public class DepartmentController : Controller
    {
        private readonly DepartmentService departmentService = new DepartmentService();

        // GET: Administrator/Department
        public ActionResult Index(string SearchTerm)
        {
            DepartmentViewModel departmentViewModel = new DepartmentViewModel
            {
                Departments = departmentService.Search(SearchTerm),
                SearchTerm = SearchTerm
            };
            return View(departmentViewModel);
        }

        [HttpGet]
        public ActionResult Action(int? ID)
        {
            DepartmentActionModel departmentActionModel = new DepartmentActionModel();
            if (ID.HasValue)
            {
                Department department = departmentService.GetDepartmentByID(ID.Value);
                departmentActionModel.ID = department.ID;
                departmentActionModel.Name = department.Name;

                departmentActionModel.Description = department.Description;
            }
            return PartialView("_Action", departmentActionModel);
        }

        [HttpGet]
        public ActionResult Delete(int ID)
        {
            Department department = departmentService.GetDepartmentByID(ID);
            DepartmentActionModel departmentActionModel = new DepartmentActionModel
            {
                ID = department.ID,
                Name = department.Name
            };
            return PartialView("_Delete", departmentActionModel);
        }

        [HttpPost]
        public ActionResult Delete(DepartmentActionModel departmentActionModel)
        {
            (bool IsTrue, string Error) = departmentService.Delete(departmentActionModel.ID);
            JsonResult jsonResult = new JsonResult
            {
                Data = new { Success = IsTrue, Msg = Error }
            };
            return jsonResult;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Action(DepartmentActionModel departmentActionModel)
        {
            JsonResult jsonResult = new JsonResult();
            if (departmentActionModel.ID > 0)
            {
                departmentActionModel.ModifiedOn = DateTimeHelper.Now();
                departmentActionModel.UserID = UserHelperInfo.GetUserId();
                departmentActionModel.IP = UserInfo.IP();
                departmentActionModel.Agent = UserInfo.Agent();
                departmentActionModel.Location = UserInfo.Location();
                var (IsSaved, Error) = departmentService.UpdateDepartment(departmentActionModel);
                jsonResult.Data = new { Success = IsSaved, Msg = Error };
            }
            else
            {
                Department department = new Department
                {
                    Name = departmentActionModel.Name,
                    Description = departmentActionModel.Description,
                    ModifiedOn = DateTimeHelper.Now(),
                    UserID = UserHelperInfo.GetUserId(),
                    IP = UserInfo.IP(),
                    Agent = UserInfo.Agent(),
                    Location = UserInfo.Location()
                };
                var (IsSaved, Error) = departmentService.SaveDepartment(department);
                jsonResult.Data = new { Success = IsSaved, Msg = Error };
            }
            return jsonResult;
        }
    }
}