using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using MySoft.Employee.Entities;
using MySoftCorporation.Areas.Administrator.Models;
using MySoftCorporation.Services;
using MySoftCorporation.Services.Helpers;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MySoftCorporation.Areas.Administrator.Controllers
{
    [Authorize(Roles = "General Manager,Administrator")]
    public class UserController : Controller
    {
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

        public UserController()
        {
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

        public UserController(UserManagerService userManager, SignManagerService signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public async Task<ActionResult> Index(string SearchTerm, string RoleID, int? Page)
        {
            int PageSize = 10;
            Page = Page ?? 1;
            UserListingModel model = new UserListingModel
            {
                SearchTerm = SearchTerm,
                RoleID = RoleID,
                Users = await Search(SearchTerm, RoleID, Page.Value, UserManager.Users.Count()),
                Pager = new Pager(0, Page, PageSize)
            };
            model.Roles = RoleManager.Roles;

            return View(model);
        }

        public async Task<IEnumerable<User>> Search(string SearchTerm, string RoleID, int Page, int RecordSize)
        {
            var User = UserManager.Users.AsQueryable();
            if (!string.IsNullOrEmpty(SearchTerm))
                User = User.Where(x => x.Email.ToLower().Contains(SearchTerm.ToLower()));

            if (!string.IsNullOrEmpty(RoleID))
            {
                var Role = await RoleManager.FindByIdAsync(RoleID);
                var UsersIDs = Role.Users.Select(x => x.UserId).ToList();
                User = User.Where(x => UsersIDs.Contains(x.Id));
            }
            return User.OrderBy(x => x.Email).ToList();
        }

        [HttpGet]
        public async Task<ActionResult> Action(string ID)
        {
            UserActionModel model = new UserActionModel();
            if (!string.IsNullOrEmpty(ID))
            {
                User objectFirst = await UserManager.FindByIdAsync(ID);
                model.ID = objectFirst.Id;
                model.UserName = objectFirst.UserName;
                model.Email = objectFirst.Email;
                model.PhoneNumber = objectFirst.PhoneNumber;
            }
            //model.Roles = provinceService.GetAll();
            return PartialView("_Action", model);
        }

        [HttpPost]
        public async Task<JsonResult> Action(UserActionModel model)
        {
            JsonResult jsonResult = new JsonResult();
            IdentityResult identityResult;
            if (!string.IsNullOrEmpty(model.ID))
            {
                var User = await UserManager.FindByIdAsync(model.ID);
                User.Id = model.ID;
                User.UserName = model.UserName;
                User.Email = model.Email;
                User.PhoneNumber = model.PhoneNumber;
                identityResult = await UserManager.UpdateAsync(User);
                jsonResult.Data = new { Success = identityResult.Succeeded, Msg = string.Join(",", identityResult.Errors) };
                return jsonResult;
            }
            else
            {
                var User = new User
                {
                    UserName = model.UserName,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber,
                };
                identityResult = await UserManager.CreateAsync(User);
                jsonResult.Data = new { Success = identityResult.Succeeded, Msg = string.Join(",", identityResult.Errors) };
                return jsonResult;
            }
        }

        [HttpGet]
        public async Task<ActionResult> Delete(string ID)
        {
            if (!string.IsNullOrEmpty(ID))
            {
                var User = await UserManager.FindByIdAsync(ID);
                UserActionModel model = new UserActionModel
                {
                    ID = User.Id,
                    UserName = User.UserName
                };
                return PartialView("_Delete", model);
            }
            else
            {
                return PartialView("_Delete");
            }
        }

        [HttpPost]
        public async Task<ActionResult> Delete(UserActionModel model)
        {
            JsonResult jsonResult = new JsonResult();
            IdentityResult identityResult;
            if (!string.IsNullOrEmpty(model.ID))
            {
                var user = await UserManager.FindByIdAsync(model.ID);
                identityResult = await UserManager.DeleteAsync(user);
                jsonResult.Data = new { Success = identityResult.Succeeded, Message = string.Join(",", identityResult.Errors) };
            }
            else
            {
                jsonResult.Data = new { Success = false, Message = "Empty Record " };
            }

            return jsonResult;
        }

        [HttpGet]
        public async Task<ActionResult> UserRoles(string ID)
        {
            UserRoleModel model = new UserRoleModel
            {
                UserID = ID
            };
            var User = await UserManager.FindByIdAsync(ID);
            var UserRoleIDs = User.Roles.Select(x => x.RoleId).ToList();
            model.UserRoles = RoleManager.Roles.Where(x => UserRoleIDs.Contains(x.Id)).ToList();
            model.Roles = RoleManager.Roles.Where(x => !UserRoleIDs.Contains(x.Id)).ToList();
            return PartialView("_UserRoles", model);
        }

        [HttpPost]
        public async Task<JsonResult> UserRoleOperation(string Operation, string UserID, string RoleID)
        {
            JsonResult jsonResult = new JsonResult();
            var User = await UserManager.FindByIdAsync(UserID);
            var Role = await RoleManager.FindByIdAsync(RoleID);
            if (User != null && Role != null)
            {
                if (Operation.Equals("Assign"))
                {
                    IdentityResult identityResult = await UserManager.AddToRoleAsync(UserID, Role.Name);
                    jsonResult.Data = new { Success = identityResult.Succeeded, Error = string.Join(",", identityResult.Errors), Msg = "Assigned Successfully!" };
                }
                else if (Operation.Equals("Delete"))
                {
                    IdentityResult identityResult = await UserManager.RemoveFromRoleAsync(UserID, Role.Name);
                    jsonResult.Data = new { Success = identityResult.Succeeded, Error = string.Join(",", identityResult.Errors), Msg = "De-Assigned Successfully!" };
                }
                else
                {
                    jsonResult.Data = new { Succes = false, Error = "Unable to Find Operation Type", Msg = "Failed!" };
                }
            }
            else
            {
                jsonResult.Data = new { Succes = false, Error = "Unable To Find User or Role", Msg = "Failed!" };
            }
            return jsonResult;
        }
    }
}