using MySoftCorporation.Areas.StudentPortal.Models;
using MySoftCorporation.Helpers;
using MySoftCorporation.Services;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MySoftCorporation.Areas.StudentPortal.Controllers
{
    public class HomeController : Controller
    {
        private readonly StudentService _studentService;
        public HomeController()
        {
            _studentService = new StudentService();
        }
        public async Task<ActionResult> Index()
        {
            string UserID = UserHelperInfo.GetUserId();
            var (admissions, _) = await _studentService.GetCoursesAsync(UserID);
            StudentModel studentModel = new StudentModel
            {
                SelectedAdmissions = admissions
            };
            return View(studentModel);
        }
    }
}