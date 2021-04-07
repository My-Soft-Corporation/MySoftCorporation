using MySoft.Employee.Entities.Helpers;
using MySoftCorporation.Areas.Administrator.Models;
using MySoftCorporation.Models;
using MySoftCorporation.Services;
using System.Web.Mvc;

namespace MySoftCorporation.Areas.Administrator.Controllers
{
    [Authorize(Roles = "General Manager,Administrator")]
    public class CityController : Controller
    {
        // GET: Administrator/City
        private readonly CityService service = new CityService();

        private readonly ProvinceService provinceService = new ProvinceService();

        public ActionResult Index(string SearchTerm)
        {
            CityViewModel model = new CityViewModel
            {
                All = service.Search(SearchTerm),
                SearchTerm = SearchTerm
            };

            return View(model);
        }

        [HttpGet]
        public ActionResult Action(int? ID)
        {
            CityActionModel model = new CityActionModel();
            if (ID.HasValue)
            {
                City objectFirst = service.GetByID(ID.Value);
                PropertyCopy.Copy(objectFirst, model);
            }
            model.Provinces = provinceService.GetAll();
            return PartialView("_Action", model);
        }

        [HttpGet]
        public ActionResult Delete(int ID)
        {
            City objectFirst = service.GetByID(ID);
            CityActionModel model = new CityActionModel();
            PropertyCopy.Copy(objectFirst, model);
            return PartialView("_Delete", model);
        }

        [HttpPost]
        public ActionResult Delete(CityActionModel model)
        {
            City objectFirst = service.GetByID(model.ID);
            bool result = service.Delete(objectFirst);
            JsonResult jsonResult = new JsonResult
            {
                Data = result ? (new { Success = true, Msg = "" }) : (new { Success = false, Msg = "Unable to Save Department" }),
            };
            return jsonResult;
        }

        [HttpPost]
        public JsonResult Action(CityActionModel model)
        {
            bool result;
            if (model.ID > 0)
            {
                City objectFirst = service.GetByID(model.ID);
                PropertyCopy.Copy(model, objectFirst);
                result = service.Update(objectFirst);
            }
            else
            {
                City objectFirst = new City
                {
                    Name = model.Name,
                    ProvinceID = model.ProvinceID
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