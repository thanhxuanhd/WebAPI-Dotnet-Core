using AutoMapper;
using Data.Abstracts;
using Data.Domain;
using Moq;
using Service.DataContracts;
using Service.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Xunit;

namespace UnitTestService.Service
{
    public class StudentServiceUnitTest : ServiceBaseTest
    {
        private IStudentRepository _studentRepository;
        private StudentService _studentService;
        private List<Student> _listStudent;

        public StudentServiceUnitTest()
        {
            _listStudent = new List<Student>()
            {
                new Student{FirstMidName="Carson",LastName="Alexander",EnrollmentDate=DateTime.Parse("2005-09-01")},
                new Student{FirstMidName="Meredith",LastName="Alonso",EnrollmentDate=DateTime.Parse("2002-09-01")},
                new Student{FirstMidName="Arturo",LastName="Anand",EnrollmentDate=DateTime.Parse("2003-09-01")},
                new Student{FirstMidName="Gytis",LastName="Barzdukas",EnrollmentDate=DateTime.Parse("2002-09-01")},
                new Student{FirstMidName="Yan",LastName="Li",EnrollmentDate=DateTime.Parse("2002-09-01")},
                new Student{FirstMidName="Peggy",LastName="Justice",EnrollmentDate=DateTime.Parse("2001-09-01")},
                new Student{FirstMidName="Laura",LastName="Norman",EnrollmentDate=DateTime.Parse("2003-09-01")},
                new Student{FirstMidName="Nino",LastName="Olivetto",EnrollmentDate=DateTime.Parse("2005-09-01")}
            };
        }
        protected override void Setup()
        {
            //Setup Mapper
            Mapper.Initialize(x =>
               x.AddProfile<DomainToDtoMapingProfile>()
           );

            //

            var mockStudentRepository = new Mock<IStudentRepository>();

            //Setup Get All
            mockStudentRepository.Setup(x => x.GetAll()).Returns(
                () => { return _listStudent.AsQueryable(); });
            //Setup FindBy
            mockStudentRepository.Setup(x => x.FindBy(It.IsAny<Expression<Func<Student, bool>>>()))
                .Returns(new Func<Expression<Func<Student, bool>>, IQueryable<Student>>(
                    (predicate) =>
                    {
                        return _listStudent.AsQueryable().Where(predicate);
                    }));
            //Setup Add
            mockStudentRepository.Setup(x => x.Add(It.IsAny<Student>())).Callback(new Action<Student>(
                (student) =>
                {
                    _listStudent.Add(student);
                }));
            //Setup Edit
            mockStudentRepository.Setup(x => x.Edit(It.IsAny<Student>())).Callback(new Action<Student>(
               (student) =>
               {
                   var itemUpdate = _listStudent
                                    .SingleOrDefault(x => x.StudentID == student.StudentID);

                   itemUpdate.LastName = student.LastName;
                   itemUpdate.FirstMidName = student.FirstMidName;
                   itemUpdate.EnrollmentDate = student.EnrollmentDate;
               }));
            //Setup Delete
            mockStudentRepository.Setup(x => x.Delete(It.IsAny<Student>())).Callback(new Action<Student>(
              (student) =>
              {
                  _listStudent.Remove(student);
              }));
            //Setup Delete

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
    }
}