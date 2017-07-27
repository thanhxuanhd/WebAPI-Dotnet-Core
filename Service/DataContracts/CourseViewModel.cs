using System.Collections.Generic;

namespace Service.DataContracts
{
    public class CourseViewModel : BaseDto
    {
        public CourseViewModel()
        {
            Enrollments = new List<EnrollmentViewModel>();
        }

        public int CourseID { get; set; }
        public string Title { get; set; }
        public int Credits { get; set; }

        public List<EnrollmentViewModel> Enrollments { get; set; }
    }
}