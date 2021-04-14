using MySoft.ErrorLoging.Entities;
using MySoft.Institute.Entities;
using MySoft.Institute.Entities.Accounts;
using MySoftCorporation.Data.Entities;
using MySoftCorporation.Services.Helpers;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Threading.Tasks;

namespace MySoftCorporation.Services
{
    public class AdmissionService
    {
        private readonly MySoftCorporationDbContext _context;

        public AdmissionService()
        {
            _context = new MySoftCorporationDbContext();
        }

        public async Task<int> GetCount()
        {
            return await _context.Admissions.CountAsync();
        }

        public async Task<IEnumerable<Admission>> GetAdmissons()
        {
            return await _context.Admissions.Include(x => x.Student).Include(x => x.Course).ToListAsync();
        }

        public Admission GetByID(int AdmissionID)
        {
            using (var mySoftCorporationDbContext = new MySoftCorporationDbContext())
            {
                return mySoftCorporationDbContext.Admissions.Where(x => x.AdmissionID == AdmissionID).SingleOrDefault();
            }
        }

        public async Task<Admission> GetLastAdmission(int StudentId)
        {
            return await _context.Admissions.Where(x => x.StudentID == StudentId)
                .OrderByDescending(x => x.AdmissionID)
                .FirstOrDefaultAsync();
        }

        public async Task<(bool IsTrue, string Msg)> ApproveAdmission(Admission admission)
        {
            using (_context)
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    _context.Entry(admission).State = EntityState.Modified;
                    StudentLedger studentLedger = new StudentLedger()
                    {
                        Title = "Account Opened",
                        Debit = 0,
                        Credit = 0,
                        Balance = 0,
                        IP = admission.IP,
                        Agent = admission.Agent,
                        Latitude = admission.Location,
                        Longitude = admission.Location,
                        StudentId = admission.StudentID
                    };
                    _context.StudentLedgers.Add(studentLedger);
                    try
                    {
                        await _context.SaveChangesAsync();
                        var Ledger = await _context.StudentLedgers.Where(x => x.StudentId == admission.StudentID).SingleOrDefaultAsync();
                        if (Ledger == null)
                        {
                            transaction.Rollback();
                            return (false, "Unable To Find Ledger");
                        }
                        else
                        {
                            StudentLedgerTransaction studentLedgerTransaction = new StudentLedgerTransaction()
                            {
                                Title = "Account Opened",
                                Debit = 0,
                                Credit = 0,
                                Balance = 0,
                                IP = admission.IP,
                                Agent = admission.Agent,
                                Latitude = admission.Location,
                                Longitude = admission.Location,
                                StudentLedgerId = Ledger.Id
                            };
                            _context.StudentLedgerTransactions.Add(studentLedgerTransaction);
                            await _context.SaveChangesAsync();
                            transaction.Commit();
                            return (true, Result.Success);
                        }
                       
                    }
                    catch (Exception exc)
                    {
                        return (false, Error.GetDetail(exc));
                    }
                }
            }
        }

        public (bool isTrue, string Msg) Admission(Student student, Admission admission)
        {
            string msg = "";
            using (var context = new MySoftCorporationDbContext())
            {
                using (DbContextTransaction transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        if (student.ID > 0)
                        {
                            context.Admissions.Add(admission);
                            context.SaveChanges();
                            transaction.Commit();
                            return (true, "You Have Successfull Applied");
                        }
                        else
                        {
                            context.Students.Add(student);
                            context.SaveChanges();
                            int ID = context.Students.OrderByDescending(p => p.ID).FirstOrDefault().ID;
                            admission.StudentID = ID;
                            context.Admissions.Add(admission);
                            context.SaveChanges();
                            transaction.Commit();
                            return (true, "You Have Successfull Applied");
                        }
                    }
                    catch (DbEntityValidationException e)
                    {
                        foreach (var eve in e.EntityValidationErrors)
                        {
                            msg += "Entity of type " + eve.Entry.Entity.GetType().Name + " in state  has the following validation errors:" + eve.Entry.State;
                            foreach (var ve in eve.ValidationErrors)
                            {
                                msg += "- Property: " + ve.PropertyName + ", Error: " + ve.ErrorMessage;
                            }
                        }
                        return (false, msg);
                    }
                    catch (Exception exc)
                    {
                        transaction.Rollback();
                        msg += exc.Message + "Inner Msg : " + exc.InnerException.InnerException.Message.ToString();
                        return (false, msg);
                    }
                }
            }
        }
    }
}