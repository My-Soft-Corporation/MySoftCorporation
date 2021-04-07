using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using MySoft.Employee.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MySoftCorporation.Services
{
    public class SignManagerService : SignInManager<User, string>
    {
        public SignManagerService(UserManagerService userManager, IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager)
        {
        }

        public override Task<ClaimsIdentity> CreateUserIdentityAsync(User user)
        {
            return user.GenerateUserIdentityAsync((UserManagerService)UserManager);
        }

        public static SignManagerService Create(IdentityFactoryOptions<SignManagerService> options, IOwinContext context)
        {
            return new SignManagerService(context.GetUserManager<UserManagerService>(), context.Authentication);
        }
    }
}
