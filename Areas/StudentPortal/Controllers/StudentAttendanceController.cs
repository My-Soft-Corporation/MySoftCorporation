using MySoft.Institute.Entities.Attendance;
using MySoftCorporation.Helpers;
using MySoftCorporation.Services;
using MySoftCorporation.Services.Services.Student;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MySoftCorporation.Areas.StudentPortal.Controllers
{
    public class StudentAttendanceController : StudentAuthorizeController
    {
        private readonly StudentService _studentService;
        private readonly StudentAttendanceService _studentAttendanceService;

        public StudentAttendanceController()
        {
            _studentService = new StudentService();
            _studentAttendanceService = new StudentAttendanceService();
        }

        // GET: Administrator/EmployeeAttendance
        [Authorize]
        public async Task<ActionResult> Index()
        {
            string UserID = UserHelperInfo.GetUserId();
            var Student = await _studentService.GetStudentByUserIdAsync(UserID);
            int StudentId = Student.ID;
            if (Student == null)
            {
                return HttpNotFound("Student Not Found In Database Only Student Can Give Attendance");
            }
            else
            {
                return View(await _studentAttendanceService.GetByStudentId(StudentId));
            }
        }

        public async Task<ActionResult> GiveAttendance()
        {
            string UserID = UserHelperInfo.GetUserId();
            var Student = await _studentService.GetStudentByUserIdAsync(UserID);
            if (Student != null)
            {
                var todayAttendance = await _studentAttendanceService.GetToday(Student.ID);
                if (todayAttendance == null)
                {
                    StudentAttendance model = new  StudentAttendance()
                    {
                        Agent = UserInfo.Agent(),
                        IP = UserInfo.IP(),
                        Latitude = "N/A",
                        Longitude = "N/A",
                        StudentId = Student.ID
                    };
                    return View("ClockIn", model);
                }
                else
                {
                    return View("ClockOut", todayAttendance);
                }
            }
            else
            {
                return new HttpNotFoundResult("Student Not Found In Database");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> ClockIn(StudentAttendance model)
        {
            model.Status = "P";
            model.ClockIn = DateTimeHelper.Now();
            model.ModifiedDate = DateTimeHelper.Now();
            model.Date = DateTime.Today;
            var (IsTrue, ResponseMSG) = await _studentAttendanceService.ClockInNow(model);
            JsonResult jsonResult = new JsonResult
            {
                Data = new { IsSuccess = IsTrue, msg = ResponseMSG }
            };
            return jsonResult;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> ClockOut(StudentAttendance model)
        {
            model.ClockOut = DateTimeHelper.Now();
            model.ModifiedDate = DateTimeHelper.Now();
            var (IsTrue, ResponseMSG) = await _studentAttendanceService.ClockOutNow(model);
            JsonResult jsonResult = new JsonResult
            {
                Data = new { IsSuccess = IsTrue, msg = ResponseMSG }
            };
            return jsonResult;
        }
    }
}