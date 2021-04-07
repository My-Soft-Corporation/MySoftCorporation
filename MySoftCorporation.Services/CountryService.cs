using MySoft.Employee.Entities.Helpers;
using MySoftCorporation.Data.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySoftCorporation.Services
{
    public class CountryService
    {
        public IEnumerable<Country> GetAll()
        {
            return new MySoftCorporationDbContext().Countries.AsEnumerable();
        }

        public IEnumerable<Country> Search(string SearchTerm)
        {
            MySoftCorporationDbContext mySoftCorporationDbContext = new MySoftCorporationDbContext();
            var objectsAll = mySoftCorporationDbContext.Countries.AsQueryable();
            if (!string.IsNullOrEmpty(SearchTerm))
                objectsAll = objectsAll.Where(a => a.Name.ToLower().Contains(SearchTerm.ToLower()));

            return objectsAll.AsEnumerable();
        }

        public Country GetByID(int ID)
        {
            return new MySoftCorporationDbContext().Countries.Find(ID);
        }

        public bool Save(Country objectFirst)
        {
            MySoftCorporationDbContext mySoftCorporationDbContext = new MySoftCorporationDbContext();
            mySoftCorporationDbContext.Countries.Add(objectFirst);
            return mySoftCorporationDbContext.SaveChanges() > 0;
        }

        public bool Update(Country objectFirst)
        {
            MySoftCorporationDbContext mySoftCorporationDbContext = new MySoftCorporationDbContext();
            mySoftCorporationDbContext.Entry(objectFirst).State = EntityState.Modified;
            return mySoftCorporationDbContext.SaveChanges() > 0;
        }

        public bool Delete(Country objectFirst)
        {
            MySoftCorporationDbContext mySoftCorporationDbContext = new MySoftCorporationDbContext();
            mySoftCorporationDbContext.Entry(objectFirst).State = EntityState.Deleted;
            return mySoftCorporationDbContext.SaveChanges() > 0;
        }
    }
}