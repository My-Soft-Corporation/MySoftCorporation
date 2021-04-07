using MySoft.Institute.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MySoftCorporation.Areas.Administrator.Models
{
    public class CourseViewModel : Course
    {
        public IEnumerable<Course> Courses { get; set; }
        public string SearchTerm { get; set; }
    }
    public class CourseActionModel : Course
    {
        public String PictureIDs { get; set; }
    }
}