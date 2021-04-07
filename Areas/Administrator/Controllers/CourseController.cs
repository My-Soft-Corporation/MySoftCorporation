using MySoft.Institute.Entities;
using MySoftCorporation.Areas.Administrator.Models;
using MySoftCorporation.Helpers;
using MySoftCorporation.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace MySoftCorporation.Areas.Administrator.Controllers
{
    [Authorize(Roles = "General Manager,Administrator")]
    public class CourseController : Controller
    {
        private readonly CourseService service = new CourseService();
        private readonly DashboardService dashboardService = new DashboardService();

        public ActionResult Index(int? courseID)
        {
            CourseViewModel courseViewModel = new CourseViewModel
            {
                Courses = service.Search(courseID)
            };
            return View(courseViewModel);
        }

        [HttpGet]
        public ActionResult Action(int? ID)
        {
            CourseActionModel model = new CourseActionModel();
            if (ID.HasValue)
            {
                Course course = service.GetByID(ID.Value);
                model.ID = course.ID;
                model.Title = course.Title;
                model.Duration = course.Duration;
                model.Fee = course.Fee;
                model.CoursePictures = course.CoursePictures;
            }
            return PartialView("_Action", model);
        }

        [HttpGet]
        public ActionResult Delete(int ID)
        {
            Course course = service.GetByID(ID);
            CourseActionModel model = new CourseActionModel
            {
                ID = course.ID,
                Title = course.Title
            };
            return PartialView("_Delete", model);
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
                    objectFirst.CoursePictures.Clear();
                    List<int> PictureListIDs = (!string.IsNullOrEmpty(model.PictureIDs) ?
                        model.PictureIDs.Split(',').Select(x => int.Parse(x)).ToList() : new List<int>());
                    var pictures = dashboardService.GetPictures(PictureListIDs);
                    objectFirst.CoursePictures = new List<CoursePicture>();
                    objectFirst.CoursePictures.AddRange(pictures.Select(x => new CoursePicture() { CourseID = model.ID, PictureID = x.ID }));
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
                    List<int> PictureListIDs = (!string.IsNullOrEmpty(model.PictureIDs) ?
                     model.PictureIDs.Split(',').Select(x => int.Parse(x)).ToList() : new List<int>());
                    var pictures = dashboardService.GetPictures(PictureListIDs);
                    objectFirst.CoursePictures = new List<CoursePicture>();
                    objectFirst.CoursePictures.AddRange(pictures.Select(x => new CoursePicture() { PictureID = x.ID }));
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