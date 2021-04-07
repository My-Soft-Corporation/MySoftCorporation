using MySoft.Institute.Entities;
using System.Collections.Generic;

namespace MySoftCorporation.Areas.Administrator.Models
{
    public class StudentViewModel : Student
    {
        public IEnumerable<Student> All{ get; set; }
        public string SearchTerm { get; set; }
    }
    public class StudentActionModel : Student
    {
        public int CourseID { get; set; }
    }
}