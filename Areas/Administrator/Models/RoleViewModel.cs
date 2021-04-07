using Microsoft.AspNet.Identity.EntityFramework;
using MySoftCorporation.Services.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MySoftCorporation.Areas.Administrator.Models
{
    public class RoleViewModel
    {
        public IEnumerable<IdentityRole> All { get; set; }
        public string SearchTerm { get; set; }
        public Pager Pager  { get; set; }
    }
    public class RoleActionModel
    {
        public string ID { get; set; }
        public string Name { get; set; }
    }
}