using MySoft.Employee.Entities;
using MySoftCorporation.Data.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySoftCorporation.Services
{
    public class DepartmentService : SuperService
    {
        public IEnumerable<Department> GetDepartments()
        {
            return new MySoftCorporationDbContext().Departments.AsEnumerable();
        }

        public IEnumerable<Department> Search(string SearchTerm)
        {
            MySoftCorporationDbContext mySoftCorporationDbContext = new MySoftCorporationDbContext();
            var department = mySoftCorporationDbContext.Departments.AsQueryable();
            if (!string.IsNullOrEmpty(SearchTerm))
                department = department.Where(a => a.Name.ToLower().Contains(SearchTerm.ToLower()));

            return department.AsEnumerable();
        }

        public Department GetDepartmentByID(int ID)
        {
            return new MySoftCorporationDbContext().Departments.Find(ID);
        }

        public (bool IsSaved, string Errors) SaveDepartment(Department department)
        {
            using (var context = new MySoftCorporationDbContext())
            {
                try
                {
                    context.Departments.Add(department);
                    return (context.SaveChanges() > 0, "Saved Successfully");
                }
                catch (DbEntityValidationException exc)
                {
                    return (false, GetValidationDetail(exc));
                }
            }
        }

        public (bool IsSaved, string Errors) UpdateDepartment(Department model)
        {
            using (var context = new MySoftCorporationDbContext())
            {
                Department department = context.Departments.SingleOrDefault(x => x.ID == model.ID);
                department.Name = model.Name;
                department.Description = model.Description;
                department.UserID = model.UserID;
                department.ModifiedOn = model.ModifiedOn;
                department.IP = model.IP;
                department.Agent = model.Agent;
                department.Location = model.Location;
                try
                {
                    context.Entry(department).State = EntityState.Modified;
                    return (context.SaveChanges() > 0, "Updated Success");
                }
                catch (DbEntityValidationException exc)
                {
                    return (false, GetValidationDetail(exc));
                }
            }
        }

        public (bool IsTrue, string Error) Delete(int ID)
        {
            using (var context = new MySoftCorporationDbContext())
            {
                try
                {
                    var department = context.Departments.SingleOrDefault(x => x.ID == ID);
                    context.Entry(department).State = EntityState.Deleted;
                    return (context.SaveChanges() > 0, "Success");
                }
                catch (Exception exc)
                {
                    return (false, GetDetail(exc));
                }
            }
        }
    }
}