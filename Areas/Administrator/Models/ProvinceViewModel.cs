using MySoft.Employee.Entities.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MySoftCorporation.Areas.Administrator.Models
{
    public class ProvinceViewModel : Province
    {
        public IEnumerable<Province> All { get; set; }
        public string SearchTerm { get; set; }
    }
    public class ProvinceActionModel: Province
    {
       
    }
}