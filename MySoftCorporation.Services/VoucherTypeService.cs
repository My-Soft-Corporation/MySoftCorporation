
using MySoft.Employee.Entities;
using MySoftCorporation.Data.Entities;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace MySoftCorporation.Services
{
    public class VoucherTypeService
    {
        public IEnumerable<VoucherType> GetAll()
        {
            var context = new MySoftCorporationDbContext();
            return context.VoucherTypes.AsEnumerable();
        }

        public IEnumerable<VoucherType> Search(string Search)
        {
            MySoftCorporationDbContext mySoftCorporationDbContext = new MySoftCorporationDbContext();
            var voucherTypes = mySoftCorporationDbContext.VoucherTypes.AsQueryable();
            voucherTypes = voucherTypes.Where(a => a.Name == Search);
            return voucherTypes.AsEnumerable();
        }

        public VoucherType GetByID(int ID)
        {
            using (var mySoftCorporationDbContext = new MySoftCorporationDbContext())
            {
                return mySoftCorporationDbContext.VoucherTypes.Find(ID);
            }
        }

        public bool Save(VoucherType voucherType)
        {
            using (var context = new MySoftCorporationDbContext())
            {
                context.VoucherTypes.Add(voucherType);
                return context.SaveChanges() > 0;
            }
        }

        public bool Update(VoucherType voucherType)
        {
            MySoftCorporationDbContext mySoftCorporationDbContext = new MySoftCorporationDbContext();
            mySoftCorporationDbContext.Entry(voucherType).State = EntityState.Modified;
            return mySoftCorporationDbContext.SaveChanges() > 0;
        }

        public bool Delete(VoucherType voucherType)
        {
            using (var context = new MySoftCorporationDbContext())
            {
                context.Entry(voucherType).State = EntityState.Deleted;
                return context.SaveChanges() > 0;
            }
        }
    }
}