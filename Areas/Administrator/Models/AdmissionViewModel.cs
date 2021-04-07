using MySoft.Institute.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MySoftCorporation.Areas.Administrator.Models
{
    public class AdmissionViewModel : Admission
    {
        public IEnumerable<Admission> Admissions { get; set; }
        public string SearchTerm { get; set; }
    }

    public class AdmissionActionModel : Admission
    {
    }
}