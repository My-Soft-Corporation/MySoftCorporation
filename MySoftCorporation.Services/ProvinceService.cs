using MySoft.Employee.Entities.Helpers;
using MySoftCorporation.Data.Entities;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;

namespace MySoftCorporation.Services
{
    public class ProvinceService
    {
        public IEnumerable<Province> GetAll()
        {
            return new MySoftCorporationDbContext().Provinces.AsEnumerable();
        }
        public IEnumerable<Province> Search(string SearchTerm)
        {
            MySoftCorporationDbContext mySoftCorporationDbContext = new MySoftCorporationDbContext();
            var objectsAll = mySoftCorporationDbContext.Provinces.AsQueryable();
            if (!string.IsNullOrEmpty(SearchTerm))
                objectsAll = objectsAll.Where(a => a.Name.ToLower().Contains(SearchTerm.ToLower()));

            return objectsAll.AsEnumerable();
        }
        public Province GetByID(int ID)
        {
            using (MySoftCorporationDbContext mySoftCorporationDbContext = new MySoftCorporationDbContext())
            {
                return mySoftCorporationDbContext.Provinces.Find(ID);
                //return  mySoftCorporationDbContext.Provinces.Include(x => x.ID).SingleOrDefault(x => x.ID = ID);
            }

        }
        public bool Save(Province objectFirst)
        {
                MySoftCorporationDbContext mySoftCorporationDbContext = new MySoftCorporationDbContext();
                mySoftCorporationDbContext.Provinces.Add(objectFirst);
                return mySoftCorporationDbContext.SaveChanges() > 0;

        }
        public bool Update(Province objectFirst)
        {
            MySoftCorporationDbContext mySoftCorporationDbContext = new MySoftCorporationDbContext();
            mySoftCorporationDbContext.Entry(objectFirst).State = EntityState.Modified;
            return mySoftCorporationDbContext.SaveChanges() > 0;
        }
        public bool Delete(Province objectFirst)
        {
            MySoftCorporationDbContext mySoftCorporationDbContext = new MySoftCorporationDbContext();
            mySoftCorporationDbContext.Entry(objectFirst).State = EntityState.Deleted;
            return mySoftCorporationDbContext.SaveChanges() > 0;
        }
    }
}
