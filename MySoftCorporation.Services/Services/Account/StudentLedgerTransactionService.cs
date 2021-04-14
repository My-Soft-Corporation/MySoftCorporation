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
   public class StudentLedgerTransactionService 
    {
        private readonly MySoftCorporationDbContext _context;
        public StudentLedgerTransactionService()
        {
            _context = new MySoftCorporationDbContext();
        }
        public async Task<List<StudentLedgerTransaction>> Get()
        {
            return await _context.StudentLedgerTransactions
                .Include(x=>x.StudentLedger)
                .Include(x=>x.StudentLedger.Student)
                .ToListAsync();
        }
        public async Task<(bool IsTrue,string ResponseMsg)> Add(StudentLedgerTransaction studentLedgerTransaction)
        {
            _context.StudentLedgerTransactions.Add(studentLedgerTransaction);
            try
            {
                return (await _context.SaveChangesAsync() > 0, Result.Success);
            }
            catch (Exception exc)
            {
                return (false, Error.GetDetail(exc));
            }
        }
    }
}
