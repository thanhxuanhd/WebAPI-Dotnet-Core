using Service.DataContracts;
using System.Collections.Generic;

namespace Service.Abstract
{
    public interface ICourseSevice
    {
        void Create(CourseViewModel course);

        void Edit(CourseViewModel course);

        void Delete(int courseId);

        List<CourseViewModel> Get(int pageSize, int pageIndex, string sortColumn, string keyWord = "", bool desc = false);

        CourseViewModel GetById(int courseId);
    }
}