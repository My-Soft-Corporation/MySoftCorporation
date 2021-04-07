using MySoft.Employee.Entities.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MySoftCorporation.Areas.Administrator.Models
{
    public class CountryViewModel : Country
    {
        public IEnumerable<Country> All { get; set; }
        public string SearchTerm { get; set; }
    }
    public class CountryActionModel : Country
    {

    }
}