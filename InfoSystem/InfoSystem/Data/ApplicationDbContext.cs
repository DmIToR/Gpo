﻿using System.Diagnostics.CodeAnalysis;
using InfoSystem.Entities;
using InfoSystem.Models;
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
    
    public DbSet<StudentProfile> Students { get; set; }
    public DbSet<EducationDepartmentProfile> EducationDepartments { get; set; }
    public DbSet<TeacherProfile> Teachers { get; set; }
    public DbSet<SecretaryProfile> Secretaries { get; set; }
}