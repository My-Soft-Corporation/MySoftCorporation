using MySoft.Institute.Entities;
using MySoftCorporation.Data.Entities;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace MySoftCorporation.Services
{
    public class CourseCategoryService
    {
        public IEnumerable<CourseCategory> GetAll()
        {
            return new MySoftCorporationDbContext().CourseCategories.AsEnumerable();
        }

        public IEnumerable<CourseCategory> Search(string SearchTerm)
        {
            MySoftCorporationDbContext mySoftCorporationDbContext = new MySoftCorporationDbContext();
            var objectsAll = mySoftCorporationDbContext.CourseCategories.AsQueryable();
            if (!string.IsNullOrEmpty(SearchTerm))
                objectsAll = objectsAll.Where(a => a.Name.ToLower().Contains(SearchTerm.ToLower()));

            return objectsAll.AsEnumerable();
        }

        public CourseCategory GetByID(int ID)
        {
            var context = new MySoftCorporationDbContext();
            return context.CourseCategories.Where(x => x.ID == ID).SingleOrDefault();
        }

        public bool Save(CourseCategory objectFirst)
        {
            MySoftCorporationDbContext mySoftCorporationDbContext = new MySoftCorporationDbContext();
            mySoftCorporationDbContext.CourseCategories.Add(objectFirst);
            return mySoftCorporationDbContext.SaveChanges() > 0;
        }

        public bool Update(CourseCategory objectFirst)
        {
            var oldContext = new MySoftCorporationDbContext();
            var oldCourseCategory = oldContext.CourseCategories.Find(objectFirst.ID);

            var NewContext = new MySoftCorporationDbContext();
            NewContext.Entry(objectFirst).State = EntityState.Modified;
            return oldContext.SaveChanges() > 0;
        }

        public bool Delete(CourseCategory objectFirst)
        {
            MySoftCorporationDbContext mySoftCorporationDbContext = new MySoftCorporationDbContext();
            mySoftCorporationDbContext.Entry(objectFirst).State = EntityState.Deleted;
            return mySoftCorporationDbContext.SaveChanges() > 0;
        }
    }
}