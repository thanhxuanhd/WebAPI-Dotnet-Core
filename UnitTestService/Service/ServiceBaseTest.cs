using AutoMapper;
using Data;
using Microsoft.EntityFrameworkCore;
using Service.Mapping;

namespace UnitTestService.Service
{
    public abstract class ServiceBaseTest
    {
        protected readonly SchoolContext _dbContext;

        public ServiceBaseTest()
        {
            //Memory DB
            var optionsBuilder = new DbContextOptionsBuilder<SchoolContext>();
            optionsBuilder.UseInMemoryDatabase();
            _dbContext = new SchoolContext(optionsBuilder.Options);
            SchoolDbInitializer.Initialize(_dbContext);

            //Setup Mapper
            Mapper.Initialize(x =>
               x.AddProfile<DomainToDtoMapingProfile>()
           );

            this.Setup();
        }

        protected abstract void Setup();
    }
}