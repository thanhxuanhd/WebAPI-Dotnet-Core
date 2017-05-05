using Data.Abstracts;
using Data.Domain;
using Data.EntiyRepositories;

namespace Data.Repositories
{
    public class StudentRepository : EntityRepository<Student>, IStudentRepository
    {
        public StudentRepository(SchoolContext entitiesContext) : base(entitiesContext)
        {
        }
    }
}