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
    public class CityService
    {
        public IEnumerable<City> GetAll()
        {
          
            return new MySoftCorporationDbContext().Cities.AsEnumerable();
            
        }
        public IEnumerable<City> Search(string SearchTerm)
        {
            MySoftCorporationDbContext mySoftCorporationDbContext = new MySoftCorporationDbContext();
            var objectsAll = mySoftCorporationDbContext.Cities.AsQueryable();
            if (!string.IsNullOrEmpty(SearchTerm))
                objectsAll = objectsAll.Where(a => a.Name.ToLower().Contains(SearchTerm.ToLower()));

            return objectsAll.AsEnumerable();
        }
        public City GetByID(int ID)
        {
            MySoftCorporationDbContext mySoftCorporationDbContext = new MySoftCorporationDbContext();
            //using (mySoftCorporationDbContext)
            //{
                return mySoftCorporationDbContext.Cities.Find(ID);
            //}
           
        }
        public bool Save(City objectFirst)
        {
                MySoftCorporationDbContext mySoftCorporationDbContext = new MySoftCorporationDbContext();
                mySoftCorporationDbContext.Cities.Add(objectFirst);
                return mySoftCorporationDbContext.SaveChanges() > 0;

        }
        public bool Update(City objectFirst)
        {
            MySoftCorporationDbContext mySoftCorporationDbContext = new MySoftCorporationDbContext();
            mySoftCorporationDbContext.Entry(objectFirst).State = EntityState.Modified;
            return mySoftCorporationDbContext.SaveChanges() > 0;
        }
        public bool Delete(City objectFirst)
        {
            MySoftCorporationDbContext mySoftCorporationDbContext = new MySoftCorporationDbContext();
            mySoftCorporationDbContext.Entry(objectFirst).State = EntityState.Deleted;
            return mySoftCorporationDbContext.SaveChanges() > 0;
        }
    }
}
