using MySoft.Employee.Entities.Attendance;
using MySoft.ErrorLoging.Entities;
using MySoftCorporation.Data.Entities;
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
        
    }
}
