using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MySoft.Employee.Entities.Attendance;
using MySoftCorporation.Data.Entities;
using MySoftCorporation.Services;
using MySoftCorporation.Helpers;
using MySoftCorporation.Services.Services;

namespace MySoftCorporation.Areas.Administrator.Controllers
{
    public class EmployeeAttendanceController : Controller
    {
        private readonly MySoftCorporationDbContext _context;
        private readonly EmployeeService _employeeService;
        private readonly EmployeeAttendanceService _employeeAttendanceService; 
        public EmployeeAttendanceController()
        {
            _employeeService = new EmployeeService();
            _context = new MySoftCorporationDbContext();
            _employeeAttendanceService = new EmployeeAttendanceService();
        }
        // GET: Administrator/EmployeeAttendance
        public async Task<ActionResult> Index()
        {
            string UserID = UserHelperInfo.GetUserId();
            var Employee = await _employeeService.GetByUserID(UserID);
            var employeeAttendances = _context.EmployeeAttendances.Where(e=>e.EmployeeId == Employee.ID).Include(e => e.Employee).Take(10);
            return View(employeeAttendances.ToList());
        }
        public async Task<ActionResult> GiveAttendance()
        {
            string UserID = UserHelperInfo.GetUserId();
            var Employee =await _employeeService.GetByUserID(UserID);
            if (Employee != null)
            {
                var todayAttendance = await _context.EmployeeAttendances.Where(x => x.Date == DateTime.Today && x.EmployeeId == Employee.ID).SingleOrDefaultAsync();
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



        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
