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
    public class CourseSevice : ICourseSevice
    {
        private ICourseRepository _courseRepository;

        public CourseSevice(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        public void Create(CourseViewModel course)
        {
            var courseEntity = Mapper.Map<CourseViewModel, Course>(course);
            _courseRepository.Add(courseEntity);

            _courseRepository.Save();
        }

        public void Delete(int courseId)
        {
            var courseEntity = _courseRepository
             .FindBy(x => x.CourseID == courseId).FirstOrDefault();

            if (courseEntity == null)
            {
                throw new Exception("Enrollment Not Found");
            }

            _courseRepository.Delete(courseEntity);
            _courseRepository.Save();
        }

        public void Edit(CourseViewModel course)
        {
            var courseEntity = _courseRepository.FindBy(x => x.CourseID == course.CourseID).FirstOrDefault();

            if (courseEntity == null)
            {
                throw new Exception("Enrollment Not Found");
            }

            courseEntity.Credits = courseEntity.Credits;
            courseEntity.Title = course.Title;

            _courseRepository.Edit(courseEntity);
            _courseRepository.Save();
        }

        public List<CourseViewModel> Get(int pageSize, int pageIndex, string sortColumn, string keyWord = "", bool desc = false)
        {
            var query = _courseRepository.GetAll();

            if (!string.IsNullOrEmpty(keyWord))
            {
                query = query.Where(x => x.Title.Contains(keyWord));
            }
            if (!string.IsNullOrEmpty(sortColumn))
            {
                query = query.OrderBy(sortColumn, desc);
            }
            else
            {
                query = query.OrderBy(x => x.Title);
            }

            var courseEntities = query.Skip(pageSize * pageIndex).Take(pageSize);

            var course = Mapper.Map<IEnumerable<Course>, IEnumerable<CourseViewModel>>(courseEntities);

            return course.ToList();
        }

        public CourseViewModel GetById(int courseId)
        {
            var courseEntity = _courseRepository
               .AllIncluding(x => x.Enrollments).Where(x => x.CourseID == courseId).FirstOrDefault();
            if (courseEntity == null)
            {
                throw new Exception("Enrollment Not Found");
            }
            var course = Mapper.Map<Course, CourseViewModel>(courseEntity);
            return course;
        }
    }
}