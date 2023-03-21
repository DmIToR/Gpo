using InfoSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace InfoSystem.Context;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options) {}
    
    public DbSet<Student> Students { get; set; }
    public DbSet<Group> Groups { get; set; }
    public DbSet<Faculty> Faculties { get; set; }
    public DbSet<AcademicPlan> AcademicPlans { get; set; }
    public DbSet<Department> Departments { get; set; }
    public DbSet<Teacher> Teachers { get; set; }
}