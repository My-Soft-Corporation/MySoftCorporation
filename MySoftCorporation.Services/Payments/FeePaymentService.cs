using MySoft.Institute.Entities.Accounts;
using MySoftCorporation.Data.Entities;
using MySoftCorporation.Services.Helpers;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySoftCorporation.Services.Payments
{
    public class FeePaymentService 
    {
        private readonly MySoftCorporationDbContext _context;
        public FeePaymentService()
        {
            _context = new MySoftCorporationDbContext();
        }
        public async Task<FeePayment> GetById(int Id)
        {
            return await _context.FeePayments.Where(x => x.Id == Id).SingleOrDefaultAsync();
        }
        public async Task<List<FeePayment>> GetByAdmissionId(int AdmissionId)
        {
            return await _context.FeePayments.Where(x=>x.AdmissionId == AdmissionId)
                .Include(x=>x.Admission)
                .Include(x=>x.PaymentGateway)
                .ToListAsync();
        }
        public async Task<List<FeePayment>> Get()
        {
            return await _context.FeePayments.Include(x=>x.PaymentGateway).Include(x=>x.Admission).Include(x=>x.Admission.Student).ToListAsync();
        }
        public async Task<(bool IsTrue, string ResponseMsg)> Save(FeePayment feePayment)
        {
            if (feePayment.Id > 0)
                _context.Entry(feePayment).State = EntityState.Modified;
            else
                _context.FeePayments.Add(feePayment);

            try
            {
                return (await _context.SaveChangesAsync() > 0, Result.Success);
            }
            catch (Exception exc)
            {
                return (false, exc.ToString());
            }
        }
    }
}
