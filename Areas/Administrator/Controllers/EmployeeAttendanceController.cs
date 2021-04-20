using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using MySoft.Employee.Entities.Attendance;
using MySoftCorporation.Services;
using MySoftCorporation.Helpers;
using MySoftCorporation.Services.Services;

namespace MySoftCorporation.Areas.Administrator.Controllers
{
    [Authorize]
    public class EmployeeAttendanceController : Controller
    {
        private EmployeeAttendanceService _employeeAttendanceService;

        public EmployeeAttendanceController()
        {
            _employeeAttendanceService = new EmployeeAttendanceService();
        }

        [Authorize]
        public async Task<ActionResult> Index()
        {
            string UserID = UserHelperInfo.GetUserId();
            if (!await _employeeAttendanceService.IsEmployee(UserID))
            {
                return HttpNotFound("Employee Not Found In Database Only Employee Can Give Attendance");
            }
            return View(await _employeeAttendanceService.GetEmployeeAttendances());
        }

        [Authorize]
        public async Task<ActionResult> GiveAttendance()
        {
            string UserID = UserHelperInfo.GetUserId();
            var Employee = await _employeeAttendanceService.GetEmployee(UserID);
            if (Employee != null)
            {
                var todayAttendance = await _employeeAttendanceService.GetTodayAttendance(Employee.ID);
                if (todayAttendance == null)
                {
                    EmployeeAttendance employeeAttendance = new EmployeeAttendance()
                    {
                        Agent = UserInfo.Agent(),
                        IP = UserInfo.IP(),
                        Latitude = "N/A",
                        Longitude = "N/A",
                        TakenById = Employee.ID,
                        EmployeeId = Employee.ID
                    };
                    return View("ClockIn", employeeAttendance);
                }
                else
                {
                    return View("ClockOut", todayAttendance);
                }
            }
            else
            {
                return new HttpNotFoundResult();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<JsonResult> ClockIn(EmployeeAttendance model)
        {
            model.Status = "N/A";
            model.ClockIn = DateTimeHelper.Now();
            model.ModifiedDate = DateTimeHelper.Now();
            model.Date = DateTime.Today;
            var (IsTrue, ResponseMSG) = await _employeeAttendanceService.ClockInNow(model);
            JsonResult jsonResult = new JsonResult
            {
                Data = new { IsSuccess = IsTrue, msg = ResponseMSG }
            };
            return jsonResult;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<JsonResult> ClockOut(EmployeeAttendance model)
        {
            model.ClockOut = DateTimeHelper.Now();
            model.ModifiedDate = DateTimeHelper.Now();
            model.Status = "P";
            var (IsTrue, ResponseMSG) = await _employeeAttendanceService.ClockOutNow(model);
            JsonResult jsonResult = new JsonResult
            {
                Data = new { IsSuccess = IsTrue, msg = ResponseMSG }
            };
            return jsonResult;
        }

        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult> Delete(int Id)
        {
            var (IsTrue, Msg) = await _employeeAttendanceService.Delete(Id);
            if (IsTrue)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return HttpNotFound(Msg);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_employeeAttendanceService != null)
                {
                    _employeeAttendanceService = null;
                }
            }

            base.Dispose(disposing);
        }
    }
}