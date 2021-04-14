using MySoft.Accounts.Entities.Models;
using MySoftCorporation.Areas.Administrator.Models;
using MySoftCorporation.Helpers;
using MySoftCorporation.Services;
using System;
using System.Web.Mvc;

namespace MySoftCorporation.Areas.Administrator.Controllers
{
    [Authorize(Roles = "General Manager,Administrator")]
    public class VoucherTypeController : Controller
    {
        // GET: Administrator/VoucherType
        private readonly VoucherTypeService service = new VoucherTypeService();
        public VoucherTypeController()
        {

        }
        public ActionResult Index(string SearchTerm)
        {
            VoucherTypeViewModel voucherTypeViewModel = new VoucherTypeViewModel
            {
                VoucherTypes = !string.IsNullOrEmpty(SearchTerm) ? service.Search(SearchTerm) : service.GetAll()
            };
            return View(voucherTypeViewModel);
        }

        [HttpGet]
        public ActionResult Delete(int ID)
        {
            VoucherType objectFirst = service.GetByID(ID);
            VoucherTypeActionModel model = new VoucherTypeActionModel
            {
                Id = objectFirst.Id,
                Name = objectFirst.Name
            };
            return PartialView("_Delete", model);
        }

        [HttpPost]
        public ActionResult Delete(CourseActionModel model)
        {
            string msg = "";
            VoucherType objectData = service.GetByID(model.ID);
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
                Data = new { Success = result, Msg = msg }
            };
            return jsonResult;
        }

        [HttpGet]
        public ActionResult Action(int? ID)
        {
            VoucherTypeActionModel model = new VoucherTypeActionModel();
            if (ID.HasValue)
            {
                VoucherType objectFirst = service.GetByID(ID.Value);
                model.Id = objectFirst.Id;
                model.Name = objectFirst.Name;
            }
            return PartialView("_Action", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Action(VoucherTypeActionModel model)
        {
            bool result;
            VoucherType voucherType = new VoucherType()
            {
                Name = model.Name,
                AddedById = UserHelperInfo.GetUserId(),
                IP = UserInfo.IP(),
                Agent = UserInfo.Agent(),
                Longitude = model.Longitude,
                Latitude = model.Latitude,
                ModifiedOn = DateTimeHelper.Now()
            };
            string msg = "";
            if (model.Id > 0)
            {
                voucherType.Id = model.Id;
                try
                {
                    result = service.Update(voucherType);
                }
                catch (Exception exc)
                {
                    msg = exc.Message.ToString();
                    result = false;
                }
            }
            else
            {
                try
                {
                    result = service.Save(voucherType);
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