using MySoft.Employee.Entities;
using MySoft.Employee.Entities.Helpers;
using System.Collections.Generic;

namespace MySoftCorporation.Areas.Administrator.Models
{
    public class EmployeeViewModel : Employee
    {
        public IEnumerable<Employee> All { get; set; }
        public string SearchTerm { get; set; }
    }
    public class EmployeeActionModel: Employee
    {
        public IEnumerable<User> Users { get; set; }
        public new IEnumerable<City> Cities { get; set; }
    }
}