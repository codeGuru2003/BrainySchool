using HealthRecordsPro.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HealthRecordsPro.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<FamilyMember>()
           .HasOne(f => f.Student)
           .WithMany()
           .OnDelete(DeleteBehavior.NoAction);
        }
        public DbSet<AcademicSchoolYear> AcademicSchoolYears { get; set; }
        public DbSet<AcademicSchoolYearType> AcademicSchoolYearTypes { get; set; }
        public DbSet<AcademicSemester> AcademicSemesters { get; set; }
        public DbSet<AcademicSemesterType> AcademicSemesterTypes { get; set; }
        public DbSet<AcademicSemesterPeriod> AcademicSemesterPeriods { get; set; }
        public DbSet<Class> Classes { get; set; }
        public DbSet<ClassBillingItems> ClassBillingItems { get; set; }
        public DbSet<ClassInstallment> ClassInstallment { get; set; }   
        public DbSet<ClassType> ClassTypes { get; set; }    
        public DbSet<GenderType> GenderTypes { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<FamilyMember> FamilyMembers { get; set; }
        public DbSet<FamilyMemberType> FamilyMemberTypes { get; set; }
        public DbSet<NationalityType> NationalityTypes { get; set;}
        public DbSet<Student> Students { get; set; }
        public DbSet<StudentType > StudentTypes { get; set; }
        public DbSet<MaritalStatusType> MaritalStatuses { get; set; }   
        public DbSet<InstallmentType> InstallmentTypes { get; set; }
        public DbSet<PeriodType> PeriodTypes { get; set; }
        public DbSet<School> Schools { get; set; }
        public DbSet<FacultyType> FacultyTypes { get; set; }
        public DbSet<Faculty> Faculties { get; set; }
        public DbSet<ClassFaculty> ClassFaculties { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Images> Images { get; set; }
        public DbSet<StudentClass> StudentClasses { get; set; }
        public DbSet<StudentGrade> StudentGrades { get; set; }

    }
}
