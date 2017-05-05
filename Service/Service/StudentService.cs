using AutoMapper;
using Data.Abstracts;
using Data.Domain;
using Service.Abstract;
using Service.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Service.DataContracts
{
    public class StudentService : IStudentService
    {
        private IStudentRepository _studentRepository;

        public StudentService(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public void Create(StudentViewModel student)
        {
            var studentEntity = Mapper.Map<StudentViewModel, Student>(student);
            _studentRepository.Add(studentEntity);

            _studentRepository.Save();
        }

        public void Delete(int studentId)
        {
            var studentEntity = _studentRepository.FindBy(x => x.StudentID == studentId).FirstOrDefault();

            if (studentEntity == null)
            {
                throw new Exception("Student Not Found");
            }

            _studentRepository.Delete(studentEntity);
            _studentRepository.Save();

        }

        public void Edit(StudentViewModel student)
        {
            var studentEntity = _studentRepository.FindBy(x => x.StudentID == student.StudentID).FirstOrDefault();

            if (studentEntity == null)
            {
                throw new Exception("Student Not Found");
            }

            studentEntity.LastName = student.LastName;
            studentEntity.FirstMidName = student.FirstMidName;
            studentEntity.EnrollmentDate = student.EnrollmentDate;

            _studentRepository.Edit(studentEntity);
            _studentRepository.Save();
        }

        public List<StudentViewModel> Get(int pageSize, int pageIndex, string sortColumn, string keyWord = "", bool desc = false)
        {
            var query = _studentRepository.GetAll();

            if (!string.IsNullOrEmpty(keyWord))
            {
                query = query.Where(x => x.FirstMidName.Contains(keyWord) | x.LastName.Contains(keyWord));
            }
            if (!string.IsNullOrEmpty(sortColumn))
            {
                query = query.OrderBy(sortColumn, desc);
            }
            else
            {
                query = query.OrderBy(x => x.FirstMidName);
            }

            var studentEntities = query.Skip(pageSize * pageIndex).Take(pageSize);

            var students = Mapper.Map<IEnumerable<Student>, IEnumerable<StudentViewModel>>(studentEntities);

            return students.ToList();
        }

        public StudentViewModel GetById(int studentId)
        {
            var studentEntitie = _studentRepository.AllIncluding(x => x.Enrollments).Where(x => x.StudentID == studentId).FirstOrDefault();
            var student = Mapper.Map<Student, StudentViewModel>(studentEntitie);
            return student;
        }
    }
}