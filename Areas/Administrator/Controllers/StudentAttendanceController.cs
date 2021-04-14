using MySoftCorporation.Services.Services.Student;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MySoftCorporation.Areas.Administrator.Controllers
{
    [RouteArea("Administrator")]
    public class StudentAttendanceController : AdminAuthorizeController
    {
        private readonly StudentAttendanceService _studentAttendanceService;
        public StudentAttendanceController()
        {
            _studentAttendanceService = new StudentAttendanceService();
        }
        // GET: Administrator/StudentAttendance
        public async Task<ActionResult> Index()
        {
            return View(await _studentAttendanceService.Get());
        }
    }
}