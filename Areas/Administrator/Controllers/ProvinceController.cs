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
    public class ProvinceController : Controller
    {
        private readonly ProvinceService service = new ProvinceService();
        private readonly CountryService countryService = new CountryService();

        public ActionResult Index(string SearchTerm)
        {
            ProvinceViewModel model = new ProvinceViewModel();

            model.All = service.Search(SearchTerm);
            model.SearchTerm = SearchTerm;

            return View(model);
        }

        [HttpGet]
        public ActionResult Action(int? ID)
        {
            ProvinceActionModel model = new ProvinceActionModel();
            if (ID.HasValue)
            {
                Province objectFirst = service.GetByID(ID.Value);
                model.ID = objectFirst.ID;
                model.Name = objectFirst.Name;
                model.CountryID = objectFirst.CountryID;
            }

            model.Countries = countryService.GetAll();
            return PartialView("_Action", model);
        }

        [HttpGet]
        public ActionResult Delete(int ID)
        {
            Province objectFirst = service.GetByID(ID);
            ProvinceActionModel model = new ProvinceActionModel();
            PropertyCopy.Copy(objectFirst, model);
            return PartialView("_Delete", model);
        }

        [HttpPost]
        public ActionResult Delete(ProvinceActionModel model)
        {
            Province objectFirst = service.GetByID(model.ID);
            bool result = service.Delete(objectFirst);
            JsonResult jsonResult = new JsonResult
            {
                Data = result ? (new { Success = true, Msg = "" }) : (new { Success = false, Msg = "Unable to Save Department" }),
            };
            return jsonResult;
        }

        [HttpPost]
        public JsonResult Action(ProvinceActionModel model)
        {
            bool result;
            if (model.ID > 0)
            {
                Province objectFirst = service.GetByID(model.ID);
                objectFirst.Name = model.Name;
                objectFirst.CountryID = model.CountryID;
                result = service.Update(objectFirst);
            }
            else
            {
                Province objectFirst = new Province();
                objectFirst.ID = model.ID;
                objectFirst.Name = model.Name;
                objectFirst.CountryID = model.CountryID;
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