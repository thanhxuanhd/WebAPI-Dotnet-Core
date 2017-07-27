using Service.DataContracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Abstract
{
    public interface IEnrollmentService
    {
        void Create(EnrollmentViewModel enrollment);

        void Edit(EnrollmentViewModel enrollment);

        void Delete(int enrollmentId);

        List<EnrollmentViewModel> Get(int pageSize, int pageIndex, string sortColumn, string keyWord = "", bool desc = false);

        EnrollmentViewModel GetById(int enrollmentId);
    }
}
