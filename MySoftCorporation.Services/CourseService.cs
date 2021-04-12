using MySoft.Institute.Entities;
using MySoftCorporation.Data.Entities;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace MySoftCorporation.Services
{
    public class CourseService
    {
        private readonly MySoftCorporationDbContext _context;
        public CourseService()
        {
            _context = new MySoftCorporationDbContext();
        }
        public async Task<List<Course>> GetAll(int Id = 0)
        {
            if (Id == 0)
            {
            return await _context.Courses.OrderBy(x=>x.CategoryID).OrderBy(x=>x.Fee).ToListAsync();
            }
            else
            {
                return await _context.Courses.Where(x=>x.ID == Id).OrderBy(x => x.CategoryID).OrderBy(x => x.Fee).ToListAsync();
            }
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
            _context.Entry(course).State = EntityState.Modified;
            return _context.SaveChanges() > 0;
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