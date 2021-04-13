using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using MySoftCorporation.Areas.Administrator.Models;
using MySoftCorporation.Services;
using MySoftCorporation.Services.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MySoftCorporation.Areas.Administrator.Controllers
{
    [Authorize(Roles = "General Manager,Administrator")]
    public class RoleController : Controller
    {
        // GET: Administrator/Role
        private SignManagerService _signInManager;

        private UserManagerService _userManager;

        public SignManagerService SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<SignManagerService>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public UserManagerService UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<UserManagerService>();
            }
            private set
            {
                _userManager = value;
            }
        }

        private RoleManagerService _roleManager;

        public RoleManagerService RoleManager
        {
            get
            {
                return _roleManager ?? HttpContext.GetOwinContext().Get<RoleManagerService>();
            }
            set
            {
                _roleManager = value;
            }
        }

        public RoleController()
        {
        }

        public RoleController(UserManagerService userManager, SignManagerService signInManager, RoleManagerService roleManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
            RoleManager = roleManager;
        }

        public ActionResult Index(string SearchTerm, string RoleID, int? Page)
        {
            int PageSize = 10;
            Page = Page ?? 1;
            RoleViewModel model = new RoleViewModel
            {
                SearchTerm = SearchTerm,
                All = Search(SearchTerm, RoleID, Page.Value, PageSize),
                Pager = new Pager(0, Page, PageSize)
            };
            return View(model);
        }

        public IEnumerable<IdentityRole> Search(string SearchTerm, string RoleID, int Page, int RecordSize)
        {
            var Roles = RoleManager.Roles.AsQueryable();
            if (!string.IsNullOrEmpty(SearchTerm))
                Roles = Roles.Where(x => x.Name.ToLower().Contains(SearchTerm.ToLower()));

            return Roles.OrderBy(x => x.Name).ToList();
        }

        [HttpGet]
        public async Task<ActionResult> Action(string ID)
        {
            RoleActionModel model = new RoleActionModel();
            if (!string.IsNullOrEmpty(ID))
            {
                var objectFirst = await RoleManager.FindByIdAsync(ID);
                model.ID = objectFirst.Id;
                model.Name = objectFirst.Name;
            }
            return PartialView("_Action", model);
        }

        [HttpPost]
        public async Task<JsonResult> Action(RoleActionModel model)
        {
            JsonResult jsonResult = new JsonResult();
            IdentityResult identityResult;
            if (!string.IsNullOrEmpty(model.ID))
            {
                var Role = await RoleManager.FindByIdAsync(model.ID);
                Role.Id = model.ID;
                Role.Name = model.Name;
                identityResult = await RoleManager.UpdateAsync(Role);
                jsonResult.Data = new { Success = identityResult.Succeeded, Msg = string.Join(",", identityResult.Errors) };
                return jsonResult;
            }
            else
            {
                IdentityRole identityRole = new IdentityRole
                {
                    Name = model.Name
                };
                identityResult = await RoleManager.CreateAsync(identityRole);
                jsonResult.Data = new { Success = identityResult.Succeeded, Msg = string.Join(",", identityResult.Errors) };
                return jsonResult;
            }
        }

        [HttpGet]
        public async Task<ActionResult> Delete(string ID)
        {
            if (!string.IsNullOrEmpty(ID))
            {
                var Role = await RoleManager.FindByIdAsync(ID);
                RoleActionModel model = new RoleActionModel
                {
                    ID = Role.Id,
                    Name = Role.Name
                };
                return PartialView("_Delete", model);
            }
            else
            {
                return PartialView("_Delete");
            }
        }

        [HttpPost]
        public async Task<ActionResult> Delete(RoleActionModel model)
        {
            JsonResult jsonResult = new JsonResult();
            IdentityResult identityResult;
            if (!string.IsNullOrEmpty(model.ID))
            {
                var Role = await RoleManager.FindByIdAsync(model.ID);
                identityResult = await RoleManager.DeleteAsync(Role);
                jsonResult.Data = new { Success = identityResult.Succeeded, Message = string.Join(",", identityResult.Errors) };
            }
            else
            {
                jsonResult.Data = new { Success = false, Message = "Empty Record " };
            }
            return jsonResult;
        }
    }
}