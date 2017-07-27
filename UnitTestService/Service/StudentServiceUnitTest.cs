using Data.Abstracts;
using Data.Domain;
using Microsoft.EntityFrameworkCore;
using Moq;
using Service.DataContracts;
using System;
using System.Linq;
using System.Linq.Expressions;
using Xunit;

namespace UnitTestService.Service
{
    public class StudentServiceUnitTest : ServiceBaseTest
    {
        private IStudentRepository _studentRepository;
        private StudentService _studentService;

        public StudentServiceUnitTest()
        {
        }

        protected override void Setup()
        {
            var mockStudentRepository = new Mock<IStudentRepository>();

            //Setup Get All
            mockStudentRepository.Setup(x => x.GetAll()).Returns(
                () => { return _dbContext.Students.AsQueryable(); });
            //Setup FindBy
            mockStudentRepository.Setup(x => x.FindBy(It.IsAny<Expression<Func<Student, bool>>>()))
                .Returns(new Func<Expression<Func<Student, bool>>, IQueryable<Student>>(
                    (predicate) =>
                    {
                        return _dbContext.Students.Where(predicate);
                    }));
            //Setup Add
            mockStudentRepository.Setup(x => x.Add(It.IsAny<Student>())).Callback(new Action<Student>(
                (student) =>
                {
                    _dbContext.Students.Add(student);
                }));
            //Setup Edit
            mockStudentRepository.Setup(x => x.Edit(It.IsAny<Student>())).Callback(new Action<Student>(
               (student) =>
               {
                   var itemUpdate = _dbContext.Students
                                    .SingleOrDefault(x => x.StudentID == student.StudentID);

                   itemUpdate.LastName = student.LastName;
                   itemUpdate.FirstMidName = student.FirstMidName;
                   itemUpdate.EnrollmentDate = student.EnrollmentDate;

                   _dbContext.Students.Update(itemUpdate);
               }));
            //Setup Delete
            mockStudentRepository.Setup(x => x.Delete(It.IsAny<Student>())).Callback(new Action<Student>(
              (student) =>
              {
                  _dbContext.Students.Remove(student);
              }));
            //Setup AllIncluding
            mockStudentRepository.Setup(x => x.AllIncluding(It.IsAny <Expression<Func<Student,object>>[]>()))
                .Returns(new Func<Expression<Func<Student, object>>[], IQueryable<Student>>(
                    (predicates) =>
                    {
                        var students = _dbContext.Students.AsQueryable();
                        foreach (var predicate in predicates)
                        {
                            students = students.Include(predicate);
                        }
                        return students;
                    }));

            ///....
            ///.... Setup repository action
            ///....

            //Call Service
            _studentRepository = mockStudentRepository.Object;

            _studentService = new StudentService(_studentRepository);
        }

        [Fact]
        public void GET_All_STUDENT_PAGING_SUCCESS()
        {
            var listStudent = _studentService.Get(10, 0, "", "");

            Assert.NotNull(listStudent);
            Assert.Equal(listStudent.Count, 8);
        }

        [Fact]
        public void GET_STUDENT_BY_ID_SUCCESS()
        {
            var student = _studentService.GetById(1);

            Assert.NotNull(student);
        }
    }
}