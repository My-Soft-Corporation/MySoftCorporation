using MySoft.Institute.Entities;
using MySoftCorporation.Data.Entities;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace MySoftCorporation.Services
{
    public class CourseService
    {
        public IEnumerable<Course> GetAll()
        {
            var context = new MySoftCorporationDbContext();
            return context.Courses.AsEnumerable();
        }

        public IEnumerable<Course> Search(int? courseID)
        {
            MySoftCorporationDbContext mySoftCorporationDbContext = new MySoftCorporationDbContext();
            var course = mySoftCorporationDbContext.Courses.AsQueryable();
            if (courseID != null)
                course = course.Where(a => a.ID == courseID);

            return course.AsEnumerable();
        }

        public Course GetByID(int ID)
        {
            MySoftCorporationDbContext mySoftCorporationDbContext = new MySoftCorporationDbContext();
            return mySoftCorporationDbContext.Courses.Find(ID);
        }

        public bool Save(Course course)
        {
            using (var context = new MySoftCorporationDbContext())
            {
                context.Courses.Add(course);
                return context.SaveChanges() > 0;
            }
        }

        public bool Update(Course course)
        {
            MySoftCorporationDbContext mySoftCorporationDbContext = new MySoftCorporationDbContext();
            var PreviousContext = mySoftCorporationDbContext.Courses.Find(course.ID);
            mySoftCorporationDbContext.CoursePictures.RemoveRange(PreviousContext.CoursePictures);
            mySoftCorporationDbContext.Entry(PreviousContext).CurrentValues.SetValues(course);
            mySoftCorporationDbContext.CoursePictures.AddRange(course.CoursePictures);
            return mySoftCorporationDbContext.SaveChanges() > 0;
        }

        public bool Delete(Course course)
        {
            using (var context = new MySoftCorporationDbContext())
            {
                context.Entry(course).State = EntityState.Deleted;
                return context.SaveChanges() > 0;
            }
        }
    }
}