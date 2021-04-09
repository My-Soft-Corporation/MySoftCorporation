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
    public class AccountTypeController : AdminAuthorizeController
    {
        private readonly AccountTypeService _service;
        public AccountTypeController()
        {
            _service = new AccountTypeService();
        }
        // GET: Administrator/AccountType
        public async Task<ActionResult> Index()
        {
            return View(await _service.GetAccountTypesAsync());
        }
        [HttpGet]
        [Authorize]
        public ActionResult Create()
        {
            AccountType accountType = new AccountType()
            {
                IP = UserInfo.IP(),
                Agent = UserInfo.Agent(),
                AddedById = UserHelperInfo.GetUserId()
            };
            return View(accountType);
        }
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(AccountType accountType)
        {
            if (ModelState.IsValid)
            {
                var (IsTrue, ResponseMsg) =  await _service.Save(accountType);
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