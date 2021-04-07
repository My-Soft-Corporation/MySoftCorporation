using MySoft.Employee.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MySoftCorporation.Areas.Administrator.Models
{
    public class DepartmentViewModel : Department
    {
        public IEnumerable<Department> Departments { get; set; }
        public string SearchTerm { get; set; }
    }

    public class DepartmentActionModel : Department
    {
    }
}