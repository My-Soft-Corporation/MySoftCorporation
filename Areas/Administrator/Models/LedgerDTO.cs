using MySoft.Accounts.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MySoftCorporation.Areas.Administrator.Models
{
    public class LedgerDTO : Ledger
    {
        public IEnumerable<Ledger> Ledgers { get; set; }
    }
}