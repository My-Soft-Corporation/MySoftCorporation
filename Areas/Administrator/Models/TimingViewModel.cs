using MySoft.Employee.Entities;
using System.Collections.Generic;

namespace MySoftCorporation.Areas.Administrator.Models
{
    public class TimingViewModel : Timing
    {
        public string SearchTerm { get; set; }
        public List<Timing> Timings { get; set; }
    }
    public class TimingActionModel : Timing
    {

    }
}