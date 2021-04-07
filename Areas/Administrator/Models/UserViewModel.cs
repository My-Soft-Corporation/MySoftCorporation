using Microsoft.AspNet.Identity.EntityFramework;
using MySoft.Employee.Entities;
using MySoftCorporation.Services.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MySoftCorporation.Areas.Administrator.Models
{
    public class UserViewModel
    {

    }
    public class UserListingModel
    {
        public IEnumerable<User> Users { get; set; }
        public string RoleID { get; set; }
        public IEnumerable<IdentityRole>  Roles{ get; set; }
        public string SearchTerm { get; set; }
        public Pager Pager{ get; set; }
    }
    public class UserActionModel 
    { 
        public string ID { get; set; }
        public string Email { get; set; }
        public string EmailConfirmed { get; set; }
        public string SecurityStamp { get; set; }
        public string PhoneNumber { get; set; }
        public string PhoneNumberConfirmed { get; set; }
        public string TwoFactorEnabled { get; set; }
        public DateTime LockoutEndDateUtc { get; set; }
        public string LockoutEnabled { get; set; }
        public string AccessFailedCount { get; set; }
        public string RoleID { get; set; }
        public IdentityRole Role { get; set; }
        public string UserName { get; set; }    
        public IEnumerable<IdentityRole> Roles { get; set; }
    }
    public class UserRoleModel
    {
        public string UserID { get; set; }
        public IEnumerable<IdentityRole> Roles { get; set; }
        public IEnumerable<IdentityRole> UserRoles { get; set; }
    }
}