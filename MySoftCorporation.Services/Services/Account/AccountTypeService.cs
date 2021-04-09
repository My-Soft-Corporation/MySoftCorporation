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
    public class AccountTypeService
    {
        private readonly MySoftCorporationDbContext _context;
        public AccountTypeService()
        {
            _context = new MySoftCorporationDbContext();
        }
        public async Task<IEnumerable<AccountType>> GetAccountTypesAsync()
        {
            return await _context.AccountTypes.Include(x => x.AddedBy).ToListAsync();
        }
        public async Task<(bool IsTrue, string ResponseMsg)> Save(AccountType accountType)
        {
            if (accountType.Id > 0)
            {
                _context.Entry(accountType).State = EntityState.Modified;
            }
            else
            {
                _context.AccountTypes.Add(accountType);
            }
            try
            {
               return( await _context.SaveChangesAsync() > 0, Result.Success);
            }
            catch (Exception exc)
            {
                return (false, Error.GetDetail(exc));

            }
        }
            
    }
}
