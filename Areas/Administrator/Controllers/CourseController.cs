using MySoft.Institute.Entities;
using MySoftCorporation.Areas.Administrator.Models;
using MySoftCorporation.Helpers;
using MySoftCorporation.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MySoftCorporation.Areas.Administrator.Controllers
{
    public class CourseController : AdminAuthorizeController
    {
        private readonly CourseService service = new CourseService();
        private readonly CourseCategoryService _service2;
        public CourseController()
        {
            _service2 = new CourseCategoryService();
        }
        public async Task<ActionResult> Index(int courseID = 0)
        {
            return View(await service.GetAll(courseID));
        }

        [HttpGet]
        public async Task<ActionResult> Action(int? ID)
        {
            ViewBag.Categories = await _service2.GetAll();
            if (ID.HasValue)
            {
                Course course = service.GetByID(ID.Value);
                course.UserID = UserHelperInfo.GetUserId();
                course.IP = UserInfo.IP();
                course.Agent = UserInfo.Agent();
                course.Location = UserInfo.Location();
                return View("_Action", course);
            }
            else
            {
                Course course = new Course
                {
                    UserID = UserHelperInfo.GetUserId(),
                    IP = UserInfo.IP(),
                    Location = UserInfo.Location(),
                    Agent = UserInfo.Agent()
                };
                return View("_Action", course);
            }
        }

        [HttpGet]
        [Authorize]
        public ActionResult Delete(int ID)
        {
            Course course = service.GetByID(ID);
            return PartialView("_Delete", course);
        }

        [HttpPost]
       
        public ActionResult Delete(CourseActionModel model)
        {
            string msg = "";
            Course objectData = service.GetByID(model.ID);
            bool result;
            try
            {
                result = service.Delete(objectData);
            }
            catch (Exception exc)
            {
                msg = exc.Message.ToString();
                result = false;
            }
            JsonResult jsonResult = new JsonResult
            {
                Data = result ? (new { Success = true, Msg = msg }) : (new { Success = false, Msg = msg }),
            };
            return jsonResult;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<ActionResult> Action(Course course)
        {
            course.ModifiedOn = DateTimeHelper.Now();
            if (ModelState.IsValid)
            {
                var (isTrue, ResponseMsg) =  await service.Save(course);
                if (isTrue)
                {
                   return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.Categories = await _service2.GetAll();
                    ModelState.AddModelError("Error", ResponseMsg);
                    return View("_Action", course);
                }
            }
            else
            {
                ViewBag.Categories = await _service2.GetAll();
                ModelState.AddModelError("Error", "check the error");
                return View("_Action", course);
            }

        }
    }
}