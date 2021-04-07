using MySoft.Employee.Entities;
using MySoftCorporation.Data.Entities;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace MySoftCorporation.Services
{
    public class CompanyService
    {
        public List<Company> GetAll()
        {
            using (var context = new MySoftCorporationDbContext())
            {
                return context.Companies.ToList();
            }
        }

        public Company GetByID(int ID)
        {
            MySoftCorporationDbContext mySoftCorporationDbContext = new MySoftCorporationDbContext();
            //using (mySoftCorporationDbContext)
            //{
            return mySoftCorporationDbContext.Companies.Find(ID);
            //}
        }

        public bool Save(Company company)
        {
            using (var context = new MySoftCorporationDbContext())
            {
                context.Companies.Add(company);
                return context.SaveChanges() > 0;
            }
        }

        public bool Update(Company company)
        {
            using (var newContext = new MySoftCorporationDbContext())
            {
                newContext.Entry(company).State = EntityState.Modified;
                return newContext.SaveChanges() > 0;
            }
        }
    }
}