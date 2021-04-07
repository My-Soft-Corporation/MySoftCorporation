using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MySoftCorporation.Areas.Administrator.Models
{
    public class DashboardModel
    {
        public int Students { get; set; }
        public int Employees { get; set; }
        public int Users { get; set; }
        public int Applications { get; set; }
    }
}