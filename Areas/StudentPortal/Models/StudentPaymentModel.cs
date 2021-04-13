using MySoft.Institute.Entities.Accounts;
using MySoft.Payment.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MySoftCorporation.Areas.StudentPortal.Models
{
    public class StudentPaymentModel
    {
        public List<FeePayment> FeePayments{ get; set; }
        public List<PaymentGateway> PaymentGateways { get; set; }
    }
}