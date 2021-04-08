using MySoft.Accounts.Entities.Models;
using MySoft.Employee.Entities;
using System.Collections.Generic;

namespace MySoftCorporation.Areas.Administrator.Models
{
    public class VoucherTypeViewModel : VoucherType
    {
        public IEnumerable<VoucherType> VoucherTypes { get; set; }
        public string SearchTerm { get; set; }
    }

    public class VoucherTypeActionModel : VoucherType
    {
    }
}