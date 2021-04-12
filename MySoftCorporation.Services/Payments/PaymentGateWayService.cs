using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MySoftCorporation.Data.Entities;
using MySoft.Payment.Entities.Models;
using MySoft.ErrorLoging.Entities;
using System.Data.Entity;
using MySoftCorporation.Services.Helpers;
using System.Linq;
using System.Data.Entity.Validation;
namespace MySoftCorporation.Services.Payments
{
    public class PaymentGateWayService
    {
        private readonly MySoftCorporationDbContext _context;
        public PaymentGateWayService()
        {
            _context = new MySoftCorporationDbContext();
        }
        public async Task<PaymentGateway> GetById(int Id)
        {
            return await _context.PaymentGateways.Where(x => x.ID == Id).SingleOrDefaultAsync();
        }
        public async Task<List<PaymentGateway>> Get()
        {
            return await _context.PaymentGateways.ToListAsync();
        }
        public async Task<(bool IsTrue, string ResponseMsg)> Save(PaymentGateway paymentGateway)
        {
            try
            {
                if (paymentGateway.ID > 0)
                {
                    _context.Entry(paymentGateway).State = EntityState.Modified;
                }
                else
                {
                    _context.PaymentGateways.Add(paymentGateway);
                }
                return (await _context.SaveChangesAsync() > 0, Result.Success);
            }
            catch(DbEntityValidationException exc)
            {
                return (false, Error.GetDetail(exc));
            }
            catch (Exception exc)
            {
                return (false, Error.GetDetail(exc));
                
            }
        }
        public async Task<(bool IsTrue, string ResponseMsg)> Delete(PaymentGateway paymentGateway)
        {
            try
            {
                _context.Entry(paymentGateway).State = EntityState.Deleted;
                return (await _context.SaveChangesAsync() > 0, Result.Success);
            }
            catch (DbEntityValidationException exc)
            {
                return (false, Error.GetDetail(exc));
            }
            catch (Exception exc)
            {
                return (false, Error.GetDetail(exc));
            }
        }

    }
}
