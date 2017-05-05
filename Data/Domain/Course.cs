using System.Collections.Generic;

namespace Data.Domain
{
    public class Course
    {
        public Course()
        {
            Enrollments = new List<Enrollment>();
        }

        public int CourseID { get; set; }
        public string Title { get; set; }
        public int Credits { get; set; }

        public virtual ICollection<Enrollment> Enrollments { get; set; }
    }
}