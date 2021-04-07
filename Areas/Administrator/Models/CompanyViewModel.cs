using MySoft.Employee.Entities;
using System.Collections.Generic;

namespace MySoftCorporation.Areas.Administrator.Models
{
    public class CompanyViewModel : Company
    {
        public List<Company> Companies { get; set; }
    }
}