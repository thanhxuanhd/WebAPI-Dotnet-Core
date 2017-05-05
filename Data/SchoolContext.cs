using Data.Domain;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class SchoolContext : DbContext
    {
        public SchoolContext(DbContextOptions<SchoolContext> options) : base(options)
        {
        }

        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<Enrollment> Enrollments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Course>().ToTable("Courses");
            modelBuilder.Entity<Course>(e =>
            {
                e.HasKey(x => x.CourseID);
                e.Property(x => x.CourseID).ValueGeneratedOnAdd();
                e.Property(x => x.Title).IsRequired(true).HasMaxLength(255);

                e.HasMany(x => x.Enrollments).WithOne(x => x.Course)
                .HasForeignKey(x => x.CourseID).HasConstraintName("FK_Course_Enrollments");
            });


            modelBuilder.Entity<Enrollment>().ToTable("Enrollments");
            modelBuilder.Entity<Enrollment>(e =>
            {
                e.HasKey(x => x.EnrollmentID);
                e.Property(x => x.EnrollmentID).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<Student>().ToTable("Students");
            modelBuilder.Entity<Student>(e =>
            {
                e.HasKey(x=>x.StudentID);
                e.Property(x => x.StudentID).ValueGeneratedOnAdd();
                e.Property(x => x.FirstMidName).IsRequired(false).HasMaxLength(255);
                e.Property(x => x.LastName).HasMaxLength(255);

                e.HasMany(x => x.Enrollments).WithOne(x => x.Student)
               .HasForeignKey(x => x.StudentID).HasConstraintName("FK_Student_Enrollments");
            });
        }
    }
}