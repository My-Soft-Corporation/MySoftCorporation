using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using MySoft.Employee.Entities.Attendance;
using MySoftCorporation.Services;
using MySoftCorporation.Helpers;
using MySoftCorporation.Services.Services;

namespace MySoftCorporation.Areas.Administrator.Controllers
{
    public class EmployeeAttendanceController : AdminAuthorizeController
    {
        private readonly EmployeeService _employeeService;
        private readonly EmployeeAttendanceService _employeeAttendanceService; 
        public EmployeeAttendanceController()
        {
            _employeeService = new EmployeeService();
            _employeeAttendanceService = new EmployeeAttendanceService();
        }
        // GET: Administrator/EmployeeAttendance
        [Authorize]
        public async Task<ActionResult> Index()
        {
            string UserID = UserHelperInfo.GetUserId();
            var Employee = await _employeeService.GetByUserID(UserID);
            if (Employee == null)
            {
                return HttpNotFound("Employee Not Found In Database Only Employee Can Give Attendance");
            }
            return View(await _employeeAttendanceService.GetEmployeeAttendances());
        }
        public async Task<ActionResult> GiveAttendance()
        {
            string UserID = UserHelperInfo.GetUserId();
            var Employee =await _employeeService.GetByUserID(UserID);
            if (Employee != null)
            {
                var todayAttendance = _employeeAttendanceService.GetTodayAttendance(Employee.ID);
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
        public async Task<JsonResult> ClockIn(EmployeeAttendance model)
        {
            model.Status = "P";
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
        public async Task<JsonResult> ClockOut(EmployeeAttendance model)
        {
            model.ClockOut = DateTimeHelper.Now();
            model.ModifiedDate = DateTimeHelper.Now();
            var (IsTrue, ResponseMSG) = await _employeeAttendanceService.ClockOutNow(model);
            JsonResult jsonResult = new JsonResult
            {
                Data = new { IsSuccess = IsTrue, msg = ResponseMSG }
            };
            return jsonResult; 
        }
    }
}
