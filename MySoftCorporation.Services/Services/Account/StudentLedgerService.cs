using MySoft.ErrorLoging.Entities;
using MySoft.Institute.Entities.Accounts;
using MySoftCorporation.Data.Entities;
using MySoftCorporation.Services.Helpers;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySoftCorporation.Services.Services.Account
{
    public class StudentLedgerService 
    {
        private readonly MySoftCorporationDbContext _context;
        public StudentLedgerService()
        {
            _context = new MySoftCorporationDbContext();
        }
        public async Task<(bool IsTrue,string ResponseMsg)> Add(StudentLedger studentLedger)
        {
            _context.StudentLedgers.Add(studentLedger);
            try
            {
                return (await _context.SaveChangesAsync() > 0, Result.Success);
            }
            catch (Exception exc)
            {
                return (false, Error.GetDetail(exc));
            }
        }
        public async Task<List<StudentLedger>> Get()
        {
            return await _context.StudentLedgers.Include(x=>x.Student).ToListAsync();
        }
    }
}
