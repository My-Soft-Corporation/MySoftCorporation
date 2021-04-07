using MySoft.Employee.Entities;
using MySoftCorporation.Data.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Threading.Tasks;

namespace MySoftCorporation.Services
{
    public class EmployeeService
    {
        private readonly MySoftCorporationDbContext _context;
        public EmployeeService()
        {
            _context = new MySoftCorporationDbContext();
        }
        public async Task<int> GetCount()
        {
            return await _context.Employees.CountAsync();
        }
        public IEnumerable<Employee> GetAll()
        {
            return new MySoftCorporationDbContext().Employees.AsEnumerable();
        }

        public IEnumerable<Employee> Search(string SearchTerm)
        {
            MySoftCorporationDbContext mySoftCorporationDbContext = new MySoftCorporationDbContext();
            var employees = mySoftCorporationDbContext.Employees.AsQueryable();
            if (!string.IsNullOrEmpty(SearchTerm))
                employees = employees.Where(a => a.FirstName.ToLower().Contains(SearchTerm.ToLower()));

            return employees.AsEnumerable();
        }

        public Employee GetByID(int ID)
        {
            using (var context = new MySoftCorporationDbContext())
            {
                return context.Employees.FirstOrDefault(x => x.ID == ID);
            }
        }
        public async Task<Employee> GetByUserID(string UserID)
        {

                return await _context.Employees.Where(x=>x.UserID == UserID).SingleOrDefaultAsync();
            
        }

        public (bool isSaved, string Error) Save(Employee employee)
        {
            using (var mySoftCorporationDbContext = new MySoftCorporationDbContext())
            {
                mySoftCorporationDbContext.Employees.Add(employee);
                return (mySoftCorporationDbContext.SaveChanges() > 0, "success");
            }
        }

        public (bool IsSaved, string Error) Update(Employee model)
        {
            var context = new MySoftCorporationDbContext();
            Employee employee = context.Employees.FirstOrDefault(x => x.ID == model.ID);
            employee.FirstName = model.FirstName;
            employee.LastName = model.LastName;
            employee.CNIC = model.CNIC;
            employee.Contact = model.Contact;
            employee.PresentAddress = model.PresentAddress;
            employee.PermenantAddress = model.PermenantAddress;
            employee.DateOfBirth = model.DateOfBirth;
            context.Entry(employee).State = EntityState.Modified;
            return (context.SaveChanges() > 0, "Success");
        }

        public bool Delete(Employee employee)
        {
            using (var mySoftCorporationDbContext = new MySoftCorporationDbContext())
            {
                mySoftCorporationDbContext.Entry(employee).State = EntityState.Deleted;
                return mySoftCorporationDbContext.SaveChanges() > 0;
            }
        }
    }
}