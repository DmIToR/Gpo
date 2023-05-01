using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace InfoSystem.Models.StudyPlanModels;

public class Course
{
    public Guid Id { get; set; }
    public Guid StudyProgramId { get; set; }
    
    public string Name { get; set; }
}