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
        private readonly DashboardService dashboardService = new DashboardService();

        public async Task<ActionResult> Index(int courseID = 0)
        {
            CourseViewModel courseViewModel = new CourseViewModel
            {
                Courses = await service.GetAll(courseID)
            };
            return View(courseViewModel);
        }

        [HttpGet]
        public ActionResult Action(int? ID)
        {
            Course model = new Course();
            if (ID.HasValue)
            {
                Course course = service.GetByID(ID.Value);
                model.ID = course.ID;
                model.Title = course.Title;
                model.Duration = course.Duration;
                model.Fee = course.Fee;
            }
            return PartialView("_Action", model);
        }

        [HttpGet]
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
        public JsonResult Action(CourseActionModel model)
        {
            bool result;
            Course objectFirst;
            string msg = "";
            if (model.ID > 0)
            {
                objectFirst = service.GetByID(model.ID);
                objectFirst.Title = model.Title;
                objectFirst.Duration = model.Duration;
                objectFirst.Fee = model.Fee;
                objectFirst.UserID = UserHelperInfo.GetUserId();
                objectFirst.ModifiedOn = DateTime.Now;
                objectFirst.IP = UserInfo.IP();
                objectFirst.Agent = UserInfo.Agent();
                objectFirst.Location = UserInfo.Location();
                try
                {
                    result = service.Update(objectFirst);
                }
                catch (Exception exc)
                {
                    msg = exc.Message.ToString();
                    result = false;
                }
            }
            else
            {
                objectFirst = new Course
                {
                    Title = model.Title,
                    Duration = model.Duration,
                    Fee = model.Fee,
                    ModifiedOn = DateTime.Now,
                    UserID = UserHelperInfo.GetUserId(),
                    IP = UserInfo.IP(),
                    Agent = UserInfo.Agent(),
                    Location = UserInfo.Location(),
                };
                try
                {
                    result = service.Save(objectFirst);
                }
                catch (Exception exc)
                {
                    msg = exc.Message.ToString();
                    result = false;
                }
            }

            JsonResult jsonResult = new JsonResult
            {
                Data = result ? (new { Success = true, Msg = msg }) : (new { Success = false, Msg = msg })
            };
            return jsonResult;
        }
    }
}