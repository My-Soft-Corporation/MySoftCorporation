using MySoftCorporation.Models;
using MySoftCorporation.Services;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MySoftCorporation.Controllers
{
    public class CourseController : Controller
    {
        readonly CourseService service ;
        public CourseController()
        {
            service = new CourseService();
        }
        // GET: Course
        public async  Task<ActionResult> Index(int? courseID)
        {
            if (courseID != null)
            {
                CourseViewModels courseViewModel = new CourseViewModels
                {
                    Courses = await service.GetAll()
                };
                return View(courseViewModel);
            }
            else
            {
                CourseViewModels courseViewModel = new CourseViewModels
                {
                    Courses = await service.GetAll()
                };
                return View(courseViewModel);
            }
           
           
        }
    }
}