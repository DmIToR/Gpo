using System.Diagnostics.CodeAnalysis;
using System.Security.Cryptography.Xml;
using InfoSystem.Entities;
using InfoSystem.Models;
using InfoSystem.Models.DepartmentModels;
using InfoSystem.Models.PracticeModels;
using InfoSystem.Models.StudyPlanModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace InfoSystem.Data;

public class ApplicationDbContext : IdentityDbContext<User, Role, Guid>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
        Database.EnsureCreated(); 
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    
        ConfigureCourse(builder);
        ConfigureStudyPlan(builder);
        ConfigureStudyType(builder);
        ConfigureDepartment(builder);
        ConfigureGroup(builder);
        ConfigurePracticeType(builder);
        ConfigurePractice(builder);
        ConfigurePracticePeriod(builder);
        ConfigureStudentGroup(builder);
        ConfigureStudent(builder);
    }
    
    public DbSet<StudyPlan> StudyPlans { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<StudyProgram> StudyPrograms { get; set; }
    public DbSet<StudyType> StudyTypes { get; set; }
    
    public DbSet<Faculty> Faculties { get; set; }
    public DbSet<Department> Departments { get; set; }
    public DbSet<Group> Groups { get; set; }
    
    public DbSet<Practice> Practices { get; set; }
    public DbSet<PracticeKind> PracticeKinds { get; set; }
    public DbSet<PracticeType> PracticeTypes { get; set; }
    public DbSet<PracticePeriod> PracticePeriods { get; set; }
    
    public DbSet<StudentGroup> StudentGroups { get; set; }
    public DbSet<Student> Students { get; set; }

    private static void ConfigureCourse(ModelBuilder builder)
    {
        builder.Entity<Course>(course =>
        {
            course.HasOne<StudyProgram>()
                .WithMany()
                .HasForeignKey(key => key.StudyProgramId)
                .IsRequired();
    
            course.HasKey(key => key.Id);
        });
    }

    private static void ConfigureStudyPlan(ModelBuilder builder)
    {
        builder.Entity<StudyPlan>(plan =>
        {
            plan.HasOne<Course>()
                .WithMany()
                .HasForeignKey(key => key.CourseId)
                .IsRequired();

            plan.HasOne<StudyType>()
                .WithOne()
                .HasForeignKey<StudyPlan>(key => key.StudyTypeId)
                .IsRequired();
            
            plan.HasOne<StudyType>()
                .WithMany()
                .HasForeignKey(key => key.FacultyId)
                .IsRequired();
            
            plan.HasKey(key => key.Id);
        });
    }

    private static void ConfigureStudyType(ModelBuilder builder)
    {
        builder.Entity<StudyType>(studyType =>
        {
            studyType.HasKey(key => key.Id);
        });
    }

    private static void ConfigureDepartment(ModelBuilder builder)
    {
        builder.Entity<Department>(department =>
        {
            department.HasOne<Faculty>()
                .WithMany()
                .HasForeignKey(key => key.FacultyId)
                .IsRequired();

            department.HasKey(key => key.Id);
        });
    }

    private static void ConfigureGroup(ModelBuilder builder)
    {
        builder.Entity<Group>(group =>
        {
            group.HasOne<Department>()
                .WithMany()
                .HasForeignKey(key => key.DepartmentId)
                .IsRequired();

            group.HasOne<StudyPlan>()
                .WithMany()
                .HasForeignKey(key => key.StudyPlanId)
                .IsRequired();

            group.HasKey(key => key.Id);
        });
    }
    
    private static void ConfigurePracticeType(ModelBuilder builder)
    {
        builder.Entity<PracticeType>(practiceType =>
        {
            practiceType.HasOne<PracticeKind>()
                .WithMany()
                .HasForeignKey(key => key.PracticeKindId)
                .IsRequired();

            practiceType.HasKey(key => key.Id);
        });
    }

    private static void ConfigurePractice(ModelBuilder builder)
    {
        builder.Entity<Practice>(practice =>
        {
            practice.HasOne<StudyPlan>()
                .WithMany()
                .HasForeignKey(key => key.StudyPlanId)
                .IsRequired();

            practice.HasOne<PracticeType>()
                .WithMany()
                .HasForeignKey(key => key.PracticeTypeId)
                .IsRequired();

            practice.HasKey(key => key.Id);
        });
    }

    private static void ConfigurePracticePeriod(ModelBuilder builder)
    {
        builder.Entity<PracticePeriod>(period =>
        {
            period.HasOne<Practice>()
                .WithMany()
                .HasForeignKey(key => key.PracticeId)
                .IsRequired();

            period.HasKey(key => key.Id);
        });
    }

    private static void ConfigureStudentGroup(ModelBuilder builder)
    {
        builder.Entity<StudentGroup>(sg =>
        {
            sg.HasOne<Group>()
                .WithMany()
                .HasForeignKey(key => key.GroupId)
                .IsRequired();

            sg.HasOne<Student>()
                .WithMany()
                .HasForeignKey(key => key.StudentId)
                .IsRequired();

            sg.HasKey(key => key.Id);
        });
    }

    private static void ConfigureStudent(ModelBuilder builder)
    {
        builder.Entity<Student>(student =>
        {
            student.HasOne<User>()
                .WithOne()
                .HasForeignKey<Student>(key => key.UserId)
                .IsRequired();

            student.HasKey(key => key.Id);
        });
    }
}