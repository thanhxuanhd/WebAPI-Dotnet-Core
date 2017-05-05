using AutoMapper;
using Data.Domain;
using Service.DataContracts;
using System.Collections.Generic;

namespace Service.Mapping
{
    public class DomainToDtoMapingProfile : Profile
    {
        public override string ProfileName => "DomainToDtoMapingProfile";

        public DomainToDtoMapingProfile()
        {
            CreateMap<Student, StudentViewModel>();
            CreateMap<StudentViewModel, Student>();
            CreateMap<Course, CourseViewModel>();
            CreateMap<Enrollment, EnrollmentViewModel>();
        }
    }
}