using MySoft.Accounts.Entities.Models;
using MySoft.ErrorLoging.Entities;
using MySoftCorporation.Data.Entities;
using MySoftCorporation.Services.Helpers;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;

namespace MySoftCorporation.Services.Services
{

    public class LedgerService
    {
        private readonly MySoftCorporationDbContext _context;
        public LedgerService()
        {
            _context = new MySoftCorporationDbContext();
        }
        public async Task<IEnumerable<Ledger>> GetLedgersAsync()
        {
            return await _context.Ledgers.Include(x=>x.User).ToListAsync();
        }
        public async Task<(bool IsTrue,string ResponseMsg)> AddAsync(Ledger ledger)
        {
            try
            {
                _context.Ledgers.Add(ledger);
                return (await _context.SaveChangesAsync() > 0, Result.Success);
            }
            catch (Exception exc)
            {
                return (false, Error.GetDetail(exc));
            }
            
        }
    }
}
