using MySoft.Employee.Entities;
using MySoftCorporation.Areas.Administrator.Models;
using MySoftCorporation.Helpers;
using MySoftCorporation.Services;
using System;
using System.Web.Mvc;

namespace MySoftCorporation.Areas.Administrator.Controllers
{
    [Authorize(Roles = "General Manager,Administrator")]
    public class CompanyController : Controller
    {
        private readonly CompanyService service = new CompanyService();

        // GET: Administrator/Company
        public ActionResult Index()
        {
            CompanyViewModel company = new CompanyViewModel()
            {
                Companies = service.GetAll()
            };
            return View(company);
        }

        [HttpPost]
        public JsonResult Save(CompanyViewModel model)
        {
            string msg = string.Empty;
            bool result;
            if (model.ID > 0)
            {
                Company objectFirst = service.GetByID(model.ID);
                objectFirst.Name = model.Name;
                objectFirst.Cell = model.Cell;
                objectFirst.Phone = model.Phone;
                objectFirst.ModifiedOn = DateTimeHelper.Now();
                objectFirst.Email = model.Email;
                objectFirst.Address = model.Address;
                objectFirst.UserID = UserHelperInfo.GetUserId();
                objectFirst.IP = UserInfo.IP();
                objectFirst.Agent = UserInfo.Agent();
                objectFirst.Location = UserInfo.Location();
                try
                {
                    result = service.Update(objectFirst);
                    msg += "";
                }
                catch (Exception exc)
                {
                    result = false;
                    msg += exc.ToString();
                }
            }
            else
            {
                Company objectFirst = new Company
                {
                    Name = model.Name,
                    Cell = model.Cell,
                    Phone = model.Phone,
                    Email = model.Email,
                    Address = model.Address,
                    ModifiedOn = DateTimeHelper.Now(),
                    UserID = UserHelperInfo.GetUserId(),
                    IP = UserInfo.IP(),
                    Agent = UserInfo.Agent(),
                    Location = UserInfo.Location()
                };
                try
                {
                    result = service.Save(objectFirst);
                }
                catch (Exception exc)
                {
                    result = false;
                    msg += exc.ToString();
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