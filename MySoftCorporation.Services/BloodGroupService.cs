using MySoft.Employee.Entities.Helpers;
using MySoftCorporation.Data.Entities;
using System.Collections.Generic;
using System.Linq;

namespace MySoftCorporation.Services
{
    public class BloodGroupService
    {
        public IEnumerable<BloodGroup> GetAll()
        {
            var context = new MySoftCorporationDbContext();
                return context.BloodGroups.AsEnumerable();
            
        }
    }
}
