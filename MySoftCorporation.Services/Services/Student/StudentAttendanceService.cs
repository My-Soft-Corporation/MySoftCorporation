using MySoft.Institute.Entities.Attendance;
using MySoftCorporation.Data.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySoft.ErrorLoging.Entities;

namespace MySoftCorporation.Services.Services.Student
{
    public class StudentAttendanceService
    {
        private readonly MySoftCorporationDbContext _context;
        public StudentAttendanceService()
        {
            _context = new MySoftCorporationDbContext();
        }
        public async Task<List<StudentAttendance>> Get()
        {
            return await _context.StudentAttendances.Include(x => x.Student).OrderByDescending(x=>x.Id).Take(50).ToListAsync();
        }
        public async Task<IEnumerable<StudentAttendance>> GetByStudentId(int StudentId)
        {
            return await _context.StudentAttendances.Include(x=>x.Student)
                .Where(s => s.StudentId == StudentId).Take(30).ToListAsync();
        }
        public async Task<StudentAttendance> GetToday(int StudentId)
        {
           return await _context.StudentAttendances.Where(x => x.Date == DateTime.Today && x.StudentId == StudentId)
                .SingleOrDefaultAsync();
        }
        public async Task<(bool IsTrue, string ResponseMSG)> ClockOutNow(StudentAttendance model)
        {
            try
            {
                _context.Entry(model).State = EntityState.Modified;
                return (await _context.SaveChangesAsync() > 0, "Success");
            }
            catch (Exception exc)
            {
                return (false, Error.GetDetail(exc));
            }
        }
        public async Task<(bool IsTrue, string ResponseMSG)> ClockInNow(StudentAttendance model)
        {
            try
            {
                _context.StudentAttendances.Add(model);
                return (await _context.SaveChangesAsync() > 0, "Success");
            }
            catch (Exception exc)
            {
                return (false, Error.GetDetail(exc));
            }
        }
    }
}
