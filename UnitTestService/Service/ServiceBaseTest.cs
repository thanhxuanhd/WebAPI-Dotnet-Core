using Data;
using Microsoft.EntityFrameworkCore;

namespace UnitTestService.Service
{
    public abstract class ServiceBaseTest
    {
        protected readonly SchoolContext DbContext;

        public ServiceBaseTest()
        {
            //Memory DB
            var optionsBuilder = new DbContextOptionsBuilder<SchoolContext>();
            optionsBuilder.UseInMemoryDatabase();
            DbContext = new SchoolContext(optionsBuilder.Options);

            this.Setup();
        }

        protected abstract void Setup();
    }
}