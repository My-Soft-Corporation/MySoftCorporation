using MySoftCorporation.Services;
using System.Web;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity.EntityFramework;
using MySoft.Employee.Entities;
using Microsoft.AspNet.Identity;

namespace MySoftCorporation.Helpers
{
    public class UserHelperInfo
    {
        public static string GetUserId()
        {
            return HttpContext.Current.User.Identity.GetUserId(); ;
        }

    }
}