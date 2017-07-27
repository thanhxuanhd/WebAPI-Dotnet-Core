using AutoMapper;
using Data.Abstracts;
using Data.Domain;
using Service.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Service.DataContracts
{
    public class EnrollmentService : IEnrollmentService
    {
        private IEnrollmentRepository _enrollmentRepository;

        public EnrollmentService(IEnrollmentRepository enrollmentRepository)
        {
            _enrollmentRepository = enrollmentRepository;
        }

        public void Create(EnrollmentViewModel enrollment)
        {
            var enrollmentEntity = Mapper.Map<EnrollmentViewModel, Enrollment>(enrollment);

            _enrollmentRepository.Add(enrollmentEntity);
            _enrollmentRepository.Save();
        }

        public void Delete(int enrollmentId)
        {
            var enrollmentEntity = _enrollmentRepository
               .FindBy(x => x.EnrollmentID == enrollmentId).FirstOrDefault();

            if (enrollmentEntity == null)
            {
                throw new Exception("Enrollment Not Found");
            }

            _enrollmentRepository.Delete(enrollmentEntity);
            _enrollmentRepository.Save();
        }

        public void Edit(EnrollmentViewModel enrollment)
        {
            var enrollmentEntity = _enrollmentRepository
                .FindBy(x => x.EnrollmentID == enrollment.EnrollmentID).FirstOrDefault();

            if (enrollmentEntity == null)
            {
                throw new Exception("Enrollment Not Found");
            }

            enrollmentEntity.Grade = enrollment.Grade;
            enrollmentEntity.CourseID = enrollment.CourseID;
            enrollmentEntity.StudentID = enrollment.StudentID;

            _enrollmentRepository.Edit(enrollmentEntity);
            _enrollmentRepository.Save();
        }

        public List<EnrollmentViewModel> Get(int pageSize, int pageIndex, string sortColumn, string keyWord = "", bool desc = false)
        {
            var query = _enrollmentRepository.AllIncluding(x => x.Student, x => x.Course);

            var enrollmentEntities = query.Skip(pageSize * pageIndex).Take(pageSize);

            var enrollments = Mapper.Map<IEnumerable<Enrollment>, IEnumerable<EnrollmentViewModel>>(enrollmentEntities);

            return enrollments.ToList();
        }

        public EnrollmentViewModel GetById(int enrollmentId)
        {
            var enrollmentEntity = _enrollmentRepository
               .FindBy(x => x.EnrollmentID == enrollmentId).FirstOrDefault();

            if (enrollmentEntity == null)
            {
                throw new Exception("Enrollment Not Found");
            }

            var enrollment = Mapper.Map<Enrollment, EnrollmentViewModel>(enrollmentEntity);

            return enrollment;
        }
    }
}