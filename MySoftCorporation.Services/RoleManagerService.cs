using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using MySoftCorporation.Data.Entities;

namespace MySoftCorporation.Services
{
    public class RoleManagerService : RoleManager<IdentityRole>
    {
        public RoleManagerService(IRoleStore<IdentityRole,string> roleStore) : base(roleStore)
        {

        }
        public static RoleManagerService Create(IdentityFactoryOptions<RoleManagerService> identityFactoryOptions,IOwinContext owingContextW)
        {
            return new RoleManagerService(new RoleStore<IdentityRole>(owingContextW.Get<MySoftCorporationDbContext>()));
        }
    }
}
