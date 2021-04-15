using MySoft.Employee.Entities.Attendance;
using MySoft.ErrorLoging.Entities;
using MySoftCorporation.Data.Entities;
using MySoftCorporation.Services.Helpers;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySoftCorporation.Services.Services
{
    public class EmployeeAttendanceService
    {
        private readonly MySoftCorporationDbContext _context;
        public EmployeeAttendanceService()
        {
            _context = new MySoftCorporationDbContext();
        }
        public async Task<EmployeeAttendance> GetTodayAttendance(int EmployeeId)
        {
           return await _context.EmployeeAttendances.Where(x => x.Date == DateTime.Today && x.EmployeeId == EmployeeId).SingleOrDefaultAsync();
        }
        public async Task<List<EmployeeAttendance>> GetEmployeeAttendances()
        {
            return await _context.EmployeeAttendances.Include(x => x.Employee).OrderByDescending(x=>x.Id).Take(50).ToListAsync();
        }
        public async Task<(bool IsTrue,string ResponseMSG)> ClockOutNow(EmployeeAttendance model)
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
        public async Task<(bool IsTrue, string ResponseMSG)> ClockInNow(EmployeeAttendance model)
        {
            try
            {              
                _context.EmployeeAttendances.Add(model);
                return (await _context.SaveChangesAsync() > 0, "Success");
            }
            catch (Exception exc)
            {
                return (false, Error.GetDetail(exc));
            }
        }
        public async Task<(bool IsTrue, String Msg)> Delete(int AttendanceId)
        {
            var Attendance = await _context.EmployeeAttendances.FindAsync(AttendanceId);
            if (Attendance != null)
            {
                _context.Entry(Attendance).State = EntityState.Deleted;
                try
                {
                    return (await _context.SaveChangesAsync() > 0, Result.Success);
                }
                catch (Exception exc)
                {
                    return (false, Error.GetDetail(exc));
                }
            }
            else
            {
                return (false, "Attendance Not Found In DB");
            }
        }
        
    }
}
