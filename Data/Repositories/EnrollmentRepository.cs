using Data.Abstracts;
using Data.Domain;
using Data.EntiyRepositories;

namespace Data.Repositories
{
    public class EnrollmentRepository : EntityRepository<Enrollment>, IEnrollmentRepository
    {
        public EnrollmentRepository(SchoolContext entitiesContext) : base(entitiesContext)
        {
        }
    }
}