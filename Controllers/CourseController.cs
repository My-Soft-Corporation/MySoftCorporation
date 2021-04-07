using MySoftCorporation.Models;
using MySoftCorporation.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MySoftCorporation.Controllers
{
    public class CourseController : Controller
    {
        readonly CourseService service = new CourseService();
        // GET: Course
        public ActionResult Index(int? courseID)
        {
            if (courseID != null)
            {
                CourseViewModels courseViewModel = new CourseViewModels
                {
                    Courses = service.Search(courseID).OrderBy(x => x.Fee)
                };
                return View(courseViewModel);
            }
            else
            {
                CourseViewModels courseViewModel = new CourseViewModels
                {
                    Courses = service.GetAll()
                };
                return View(courseViewModel);
            }
           
           
        }
    }
}