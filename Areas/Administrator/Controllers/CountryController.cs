using MySoft.Employee.Entities.Helpers;
using MySoftCorporation.Areas.Administrator.Models;
using MySoftCorporation.Models;
using MySoftCorporation.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MySoftCorporation.Areas.Administrator.Controllers
{
    [Authorize(Roles = "General Manager,Administrator")]
    public class CountryController : Controller
    {
        // GET: Administrator/Country
        // GET: Administrator/Student
        private readonly CountryService service = new CountryService();

        public ActionResult Index(string SearchTerm)
        {
            CountryViewModel model = new CountryViewModel
            {
                All = service.Search(SearchTerm),
                SearchTerm = SearchTerm
            };
            return View(model);
        }

        [HttpGet]
        public ActionResult Action(int? ID)
        {
            CountryActionModel model = new CountryActionModel();
            if (ID.HasValue)
            {
                Country objectFirst = service.GetByID(ID.Value);
                PropertyCopy.Copy(objectFirst, model);
            }
            return PartialView("_Action", model);
        }

        [HttpGet]
        public ActionResult Delete(int ID)
        {
            Country objectFirst = service.GetByID(ID);
            CountryActionModel model = new CountryActionModel();
            PropertyCopy.Copy(objectFirst, model);
            return PartialView("_Delete", model);
        }

        [HttpPost]
        public ActionResult Delete(CountryActionModel model)
        {
            Country objectFirst = service.GetByID(model.ID);
            bool result = service.Delete(objectFirst);
            JsonResult jsonResult = new JsonResult
            {
                Data = result ? (new { Success = true, Msg = "" }) : (new { Success = false, Msg = "Unable to Save Department" }),
            };
            return jsonResult;
        }

        [HttpPost]
        public JsonResult Action(CountryActionModel model)
        {
            bool result;
            if (model.ID > 0)
            {
                Country objectFirst = service.GetByID(model.ID);
                PropertyCopy.Copy(model, objectFirst);
                result = service.Update(objectFirst);
            }
            else
            {
                Country objectFirst = new Country();
                PropertyCopy.Copy(model, objectFirst);
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