using MySoft.Accounts.Entities.Models.Accounts;
using MySoft.ErrorLoging.Entities;
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
   public class ChartOfAccountService
    {
        private readonly MySoftCorporationDbContext _context;
        public ChartOfAccountService()
        {
            _context = new MySoftCorporationDbContext();
        }
        public async Task<IEnumerable<ChartOfAccount>> GetAccountTypesAsync()
        {
            return await _context.ChartOfAccounts.Include(x => x.User).Include(x=>x.AccountType).ToListAsync();
        }
        public async Task<(bool IsTrue, string ResponseMsg)> Save(ChartOfAccount accountType)
        {
            if (accountType.Id > 0)
            {
                _context.Entry(accountType).State = EntityState.Modified;
            }
            else
            {
                _context.ChartOfAccounts.Add(accountType);
            }
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
