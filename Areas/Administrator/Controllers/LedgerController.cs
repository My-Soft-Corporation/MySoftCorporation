using MySoft.Accounts.Entities.Models;
using MySoftCorporation.Areas.Administrator.Models;
using MySoftCorporation.Helpers;
using MySoftCorporation.Services.Services;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MySoftCorporation.Areas.Administrator.Controllers
{

    public class LedgerController : AdminAuthorizeController
    {
        private readonly LedgerService _ledger;
        public LedgerController()
        {
            _ledger = new LedgerService();
        }
        // GET: Administrator/Ledger
        public async Task<ActionResult> Index()
        {
            LedgerDTO model = new LedgerDTO()
            {
                Ledgers = await _ledger.GetLedgersAsync()
            };
            return View(model);
        }
        [HttpGet]
        public ActionResult Action()
        {
            return PartialView("_Action",new Ledger());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<ActionResult> Action(Ledger ledger)
        {
            ledger.UserID = UserHelperInfo.GetUserId();
            ledger.IP = UserInfo.IP();
            ledger.Agent = UserInfo.Agent();    
            var (IsTrue, ResponseMsg) = await _ledger.AddAsync(ledger);
            if (IsTrue)
            {
                return RedirectToAction("Index");
            }
            else
            {
                LedgerDTO model = new LedgerDTO
                {
                    Ledgers = await _ledger.GetLedgersAsync()
                };
                ViewBag.Message = ResponseMsg;
                return View("Index",model.Ledgers);
            }

        }
    }
}