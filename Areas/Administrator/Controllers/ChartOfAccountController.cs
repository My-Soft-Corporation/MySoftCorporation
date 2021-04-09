using MySoft.Accounts.Entities.Models.Accounts;
using MySoftCorporation.Helpers;
using MySoftCorporation.Services.Services.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MySoftCorporation.Areas.Administrator.Controllers
{
    public class ChartOfAccountController : Controller
    {
        private readonly ChartOfAccountService _service;
        private readonly AccountTypeService accountTypeService;
        public ChartOfAccountController()
        {
            _service = new ChartOfAccountService();
            accountTypeService = new AccountTypeService();
        }
        public async Task<ActionResult> Index()
        {

            return View(await _service.GetAccountTypesAsync());
        }
        [HttpGet]
        [Authorize]
        public async Task<ActionResult> Create()
        {
            ChartOfAccount accountType = new ChartOfAccount()
            {
                IP = UserInfo.IP(),
                Agent = UserInfo.Agent(),
                UserID = UserHelperInfo.GetUserId()
            };
            ViewBag.AccountTypesList = await accountTypeService.GetAccountTypesAsync();
            return View(accountType);
        }
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ChartOfAccount accountType)
        {
            if (ModelState.IsValid)
            {
                var (IsTrue, ResponseMsg) = await _service.Save(accountType);
                if (IsTrue && ResponseMsg == "Success")
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("Unique", ResponseMsg);
                    return View(accountType);
                }
            }
            return View(accountType);
        }
    }
}