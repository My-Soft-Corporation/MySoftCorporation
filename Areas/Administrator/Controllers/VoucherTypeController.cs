using MySoft.Employee.Entities;
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
                Data = result ? (new { Success = true, Msg = msg }) : (new { Success = false, Msg = msg }),
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
        public JsonResult Action(VoucherTypeActionModel model)
        {
            bool result;
            VoucherType objectFirst;
            string msg = "";
            if (model.Id > 0)
            {
                objectFirst = service.GetByID(model.Id);
                objectFirst.Id= model.Id;
                objectFirst.Name = model.Name;
                objectFirst.AddedById = UserHelperInfo.GetUserId();
                objectFirst.DateTime = DateTime.Now;
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
                objectFirst = new VoucherType
                {
                    Name = model.Name,
                    DateTime = DateTime.Now,
                    AddedById = UserHelperInfo.GetUserId(),
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