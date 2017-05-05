using AutoMapper;
using Data;
using Data.Abstracts;
using Data.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Serialization;
using Service.Abstract;
using Service.DataContracts;
using Service.Mapping;

namespace WebApi
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add context
            services.AddDbContext<SchoolContext>(opstions => opstions
            .UseSqlServer(Configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("WebApi")));
            // ----===========---- Dependency Injection ----===========----
            // Add Repository
            services.AddScoped<IStudentRepository, StudentRepository>();
            services.AddScoped<ICourseRepository, CourseRepository>();
            services.AddScoped<IEnrollmentRepository, EnrollmentRepository>();

            services.AddScoped<IStudentService, StudentService>();
            services.AddScoped<IEnrollmentService, EnrollmentService>();
            services.AddScoped<ICourseSevice, CourseSevice>();

            //Add Mapping
            Mapper.Initialize(x =>
                x.AddProfile<DomainToDtoMapingProfile>()
            );
            services.AddCors(options => options.AddPolicy("AllowCors",
                            builder =>{builder.AllowAnyOrigin().WithMethods("GET", "PUT", "POST", "DELETE").AllowAnyHeader();}));
            // Add framework services.
            services.AddMvc().AddJsonOptions(opts =>
            {
                opts.SerializerSettings.ContractResolver = new DefaultContractResolver();
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, SchoolContext context)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseMvc();
            //Add Data To DB
            SchoolDbInitializer.Initialize(context);
        }
    }
}