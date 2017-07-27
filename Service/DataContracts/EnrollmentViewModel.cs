using Data.Domain;

namespace Service.DataContracts
{
    public class EnrollmentViewModel : BaseDto
    {
        public int EnrollmentID { get; set; }

        public int CourseID { get; set; }

        public int StudentID { get; set; }

        public Grade? Grade { get; set; }
    }
}