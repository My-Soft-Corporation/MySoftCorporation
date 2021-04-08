using MySoft.Employee.Entities;
using MySoftCorporation.Areas.Administrator.Models;
using MySoftCorporation.Data.Entities;
using MySoftCorporation.Helpers;
using MySoftCorporation.Services;
using System;
using System.Data.Entity.Validation;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MySoftCorporation.Areas.Administrator.Controllers
{
    [Authorize(Roles = "General Manager,Administrator")]
    public class TimingsController : Controller
    {
        private readonly MySoftCorporationDbContext db = new MySoftCorporationDbContext();
        private readonly TimingService service = new TimingService();

        // GET: Administrator/Timings
        public async Task<ActionResult> Index()
        {
            //var timings = db.Timings.Include(t => t.User);
            TimingViewModel timingViewModel = new TimingViewModel
            {
                Timings = await service.Get()
            };
            return View(timingViewModel);
        }

        // GET: Administrator/Timings/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Timing timing = await db.Timings.FindAsync(id);
            if (timing == null)
            {
                return HttpNotFound();
            }
            return View(timing);
        }

        [HttpGet]
        public async Task<ActionResult> Action(int? ID)
        {
            TimingActionModel model = new TimingActionModel();
            if (ID.HasValue)
            {
                Timing timing = await service.Details(ID.Value);
                model.ID = timing.ID;
                model.Day = timing.Day;
                model.Number = timing.Number;
                model.OpeningTime = timing.OpeningTime;
                model.ClosingTime = timing.ClosingTime;
                model.ModifiedOn = DateTime.Now;
                model.UserID = UserHelperInfo.GetUserId();
                model.IP = UserInfo.IP();
                model.Agent = UserInfo.Agent();
                model.Location = UserInfo.Location();
            }
            else
            {
            }
            return PartialView("_Action", model);
        }

        [HttpPost]
        public JsonResult Action(TimingActionModel model)
        {
            bool result;
            string msg = "";
            JsonResult jsonResult = new JsonResult();

            if (model.ID == 0)
            {
                Timing timing = new Timing
                {
                    Day = model.Day,
                    Number = model.Number,
                    OpeningTime = model.OpeningTime,
                    ClosingTime = model.ClosingTime,
                    ModifiedOn = DateTime.Now,
                    UserID = UserHelperInfo.GetUserId(),
                    IP = UserInfo.IP(),
                    Agent = UserInfo.Agent(),
                    Location = UserInfo.Location()
                };
                try
                {
                    result = service.Save(timing);
                    msg += "success";
                }
                catch (DbEntityValidationException e)
                {
                    result = false;
                    foreach (var eve in e.EntityValidationErrors)
                    {
                        foreach (var ve in eve.ValidationErrors)
                            msg += "" + string.Format("- Property: \"{0}\", Error: \"{1}\"", ve.PropertyName, ve.ErrorMessage);
                    }
                }
                catch (Exception exc)
                {
                    msg += exc.Message.ToString() + " Detail : " + exc.InnerException.ToString();
                    result = false;
                }
                jsonResult.Data = result ? (new { Success = result, Msg = msg }) : (new { Success = false, Msg = msg });
            }
            else
            {
                Timing timing = new Timing
                {
                    ID = model.ID,
                    Day = model.Day,
                    Number = model.Number,
                    OpeningTime = model.OpeningTime,
                    ClosingTime = model.ClosingTime,
                    ModifiedOn = DateTime.Now,
                    UserID = UserHelperInfo.GetUserId(),
                    IP = UserInfo.IP(),
                    Agent = UserInfo.Agent(),
                    Location = UserInfo.Location()
                };
                try
                {
                    result = service.Update(timing);
                    msg += "success";
                }
                catch (DbEntityValidationException e)
                {
                    result = false;
                    foreach (var eve in e.EntityValidationErrors)
                    {
                        foreach (var ve in eve.ValidationErrors)
                            msg += "" + string.Format("- Property: \"{0}\", Error: \"{1}\"", ve.PropertyName, ve.ErrorMessage);
                    }
                }
                catch (Exception exc)
                {
                    msg += exc.Message.ToString() + " Detail : " + exc.InnerException.ToString();
                    result = false;
                }
                jsonResult.Data = result ? (new { Success = result, Msg = msg }) : (new { Success = result, Msg = msg });
            }

            return jsonResult;
        }
    }
}