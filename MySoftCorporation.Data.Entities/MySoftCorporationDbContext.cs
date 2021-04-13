using Microsoft.AspNet.Identity.EntityFramework;
using MySoft.Account.Entities;
using MySoft.Accounts.Entities.Models;
using MySoft.Accounts.Entities.Models.Accounts;
using MySoft.Employee.Entities;
using MySoft.Employee.Entities.Attendance;
using MySoft.Employee.Entities.Helpers;
using MySoft.Institute.Entities;
using MySoft.Institute.Entities.Accounts;
using MySoft.Institute.Entities.Models;
using MySoft.Payment.Entities.Models;
using System.Data.Entity;

namespace MySoftCorporation.Data.Entities
{
    public class MySoftCorporationDbContext : IdentityDbContext<User>
    {
        public MySoftCorporationDbContext() : base("ServerOnServer")
        {
        }

        public static MySoftCorporationDbContext Create()
        {
            return new MySoftCorporationDbContext();
        }

        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<EmployeeAttendance> EmployeeAttendances{ get; set; }
        public DbSet<Term> Terms { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<BloodGroup> BloodGroups { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Province> Provinces { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Timing> Timings { get; set; }
        public DbSet<Picture> Pictures { get; set; }
        public DbSet<CoursePicture> CoursePictures { get; set; }
        public DbSet<UserPicture> UserPictures { get; set; }
        public DbSet<EmployeePicture> EmployeePictures { get; set; }
        public DbSet<Admission> Admissions { get; set; }
        #region Accounts
        public DbSet<AccountType> AccountTypes { get; set; }
        public DbSet<Ledger> Ledgers { get; set; }
        public DbSet<Voucher> Vouchers { get; set; }
        public DbSet<VoucherType> VoucherTypes { get; set; }
        public DbSet<ChartOfAccount> ChartOfAccounts { get; set; }
        public DbSet<PaymentGateway> PaymentGateways { get; set; }
        public DbSet<FeePayment> FeePayments { get; set; }
        #endregion

        public DbSet<CourseCategory> CourseCategories { get; set; }
        public DbSet<CourseTiming> CourseTimings { get; set; }
    }
}