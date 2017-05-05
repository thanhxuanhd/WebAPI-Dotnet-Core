using Service.DataContracts;
using System.Collections.Generic;

namespace Service.Abstract
{
    public interface IStudentService
    {
        void Create(StudentViewModel student);

        void Edit(StudentViewModel student);

        void Delete(int studentId);

        List<StudentViewModel> Get(int pageSize, int pageIndex, string sortColumn, string keyWord = "", bool desc = false);

        StudentViewModel GetById(int studentId);
    }
}