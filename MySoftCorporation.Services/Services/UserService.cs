using MySoftCorporation.Data.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySoftCorporation.Services.Services
{
    public class UserService
    {
        private readonly MySoftCorporationDbContext _context;
        public UserService()
        {
            _context = new MySoftCorporationDbContext();
        }
        public async Task<int> GetCount()
        {
            return await _context.Users.CountAsync();
        }
    }
}
