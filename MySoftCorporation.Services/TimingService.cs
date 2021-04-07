using MySoft.Employee.Entities;
using MySoftCorporation.Data.Entities;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;

namespace MySoftCorporation.Services
{
    public class TimingService
    {
        public async Task<List<Timing>> Get()
        {
            MySoftCorporationDbContext mySoftCorporationDbContext = new MySoftCorporationDbContext();
            var timings = mySoftCorporationDbContext.Timings.Include(t => t.User);
            return await timings.ToListAsync();
        }

        public async Task<Timing> Details(int? id)
        {
            if (id == null)
            {
                return null;
            }
            using (var mySoftCorporationDbContext = new MySoftCorporationDbContext())
            {
                Timing timing = await mySoftCorporationDbContext.Timings.FindAsync(id);
                return timing;
            }
        }

        public  bool Update(Timing timing)
        {
            using (var mySoftCorporationDbContext = new MySoftCorporationDbContext())
            {
                mySoftCorporationDbContext.Entry(timing).State = EntityState.Modified;
                return  mySoftCorporationDbContext.SaveChanges() > 0;
            }
        }

        public bool Save(Timing timing)
        {
            using (var context = new MySoftCorporationDbContext())
            {
                context.Timings.Add(timing);
                return  context.SaveChanges() > 0;
            }
        }
        public bool Delete(Timing timing)
        {
            using (var context = new MySoftCorporationDbContext())
            {
                context.Entry(timing).State = EntityState.Deleted;
                return context.SaveChanges() > 0;
            }

        }
    }
}