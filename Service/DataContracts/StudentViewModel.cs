using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Service.DataContracts
{
    public class StudentViewModel : BaseDto
    {
        public StudentViewModel()
        {
            Enrollments = new List<EnrollmentViewModel>();
        }

        public int StudentID { get; set; }

        [Required(ErrorMessage = "Last Name is requred")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "First Name is requred")]
        public string FirstMidName { get; set; }

        public DateTime EnrollmentDate { get; set; }

        public List<EnrollmentViewModel> Enrollments { get; set; }
    }
}