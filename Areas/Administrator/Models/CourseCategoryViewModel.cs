using MySoft.Institute.Entities;
using System.Collections;
using System.Collections.Generic;

namespace MySoftCorporation.Areas.Administrator.Models
{
    public class CourseCategoryViewModel : CourseCategory
    {
        public string SearchTerm { get; set; }
        public IEnumerable<CourseCategory> All { get; set; }
    }

    public class CourseCategoryActionModel : CourseCategory
    {
    }
}