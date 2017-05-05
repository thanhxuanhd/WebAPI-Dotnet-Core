using Data.Abstracts;
using Data.Domain;
using Data.EntiyRepositories;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public class CourseRepository : EntityRepository<Course>, ICourseRepository
    {
        public CourseRepository(SchoolContext entitiesContext) : base(entitiesContext)
        {
        }
    }
}
