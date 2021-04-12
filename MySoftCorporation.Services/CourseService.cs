using MySoft.ErrorLoging.Entities;
using MySoft.Institute.Entities;
using MySoftCorporation.Data.Entities;
using MySoftCorporation.Services.Helpers;
using System;
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
        public async Task<IEnumerable<Course>> GetAll(int Id = 0)
        {
            if (Id == 0)
            {
                return await _context.Courses.
                        Include(x=>x.CourseCategory).
                        Include(x=>x.User)
                        .OrderBy(x=>x.Fee)
                        .ToListAsync();
            }
            else
            {
                return await _context.Courses.
                                        Include(x => x.CourseCategory).
                                        Include(x => x.User).
                                        Where(x=>x.ID == Id).
                                        OrderBy(x => x.Fee).
                                        ToListAsync();
            }
        }
        public Course GetByID(int ID)
        {
            return _context.Courses.Where(x=>x.ID == ID).SingleOrDefault();
        }

        public async Task<(bool isTrue,string ResponseMsg)> Save(Course course)
        {
            if (course.ID > 0)
                _context.Entry(course).State = EntityState.Modified;
            else
                _context.Courses.Add(course);
            try
            {
               return (await _context.SaveChangesAsync() > 0, Result.Success);
            }
            catch (Exception exc)
            {
                return (false, Error.GetDetail(exc));
            }
        }

        public bool Delete(Course course)
        {
            var selectedCourse = _context.Courses.Where(x => x.ID == course.ID);
                _context.Entry(selectedCourse).State = EntityState.Deleted;
                return _context.SaveChanges() > 0;
        }
    }
}