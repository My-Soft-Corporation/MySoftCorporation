using MySoft.Institute.Entities;
using MySoftCorporation.Areas.Administrator.Models;
using MySoftCorporation.Helpers;
using MySoftCorporation.Models;
using MySoftCorporation.Services;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MySoftCorporation.Areas.Administrator.Controllers
{
    public class CourseCatagoryController : AdminAuthorizeController
    {
        // GET: Administrator/CourseCatagory
        private readonly CourseCategoryService service = new CourseCategoryService();

        public async Task<ActionResult> Index()
        {        
            return View(await service.GetAll());
        }

        [HttpGet]
        public ActionResult Action(int? ID)
        {
            CourseCategoryActionModel model = new CourseCategoryActionModel();
            if (ID.HasValue)
            {
                CourseCategory objectFirst = service.GetByID(ID.Value);
                PropertyCopy.Copy(objectFirst, model);
            }
            return PartialView("_Action", model);
        }

        [HttpGet]
        public ActionResult Delete(int ID)
        {
            CourseCategory objectFirst = service.GetByID(ID);
            CourseCategoryActionModel model = new CourseCategoryActionModel();
            PropertyCopy.Copy(objectFirst, model);
            return PartialView("_Delete", model);
        }

        [HttpPost]
        public ActionResult Delete(CountryActionModel model)
        {
            CourseCategory objectFirst = service.GetByID(model.ID);
            bool result = service.Delete(objectFirst);
            JsonResult jsonResult = new JsonResult
            {
                Data = result ? (new { Success = true, Msg = "" }) : (new { Success = false, Msg = "Unable to Save Department" }),
            };
            return jsonResult;
        }

        [HttpPost]
        public JsonResult Action(CourseCategoryActionModel model)
        {
            bool result;
            if (model.ID > 0)
            {
                CourseCategory objectFirst = service.GetByID(model.ID);
                objectFirst.ID = model.ID;
                objectFirst.Name = model.Name;
                objectFirst.UserID = UserHelperInfo.GetUserId();
                objectFirst.ModifiedOn = DateTimeHelper.Now();
                objectFirst.IP = UserInfo.IP();
                objectFirst.Agent = UserInfo.Agent();
                objectFirst.Location = UserInfo.Location();
                result = service.Update(objectFirst);
            }
            else
            {
                CourseCategory objectFirst = new CourseCategory
                {
                    Name = model.Name,
                    UserID = UserHelperInfo.GetUserId(),
                    ModifiedOn = DateTimeHelper.Now(),
                    IP = UserInfo.IP(),
                    Agent = UserInfo.Agent(),
                    Location = UserInfo.Location()
                };

                result = service.Save(objectFirst);
            }

            JsonResult jsonResult = new JsonResult
            {
                Data = result ? (new { Success = true, Msg = "" }) : (new { Success = false, Msg = "Unable to Save Course" })
            };
            return jsonResult;
        }
    }
}