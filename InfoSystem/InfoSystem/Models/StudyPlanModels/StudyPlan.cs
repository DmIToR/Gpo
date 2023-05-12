using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace InfoSystem.Models.StudyPlanModels;

public class StudyPlan
{
    public Guid Id { get; set; }
    public Guid CourseId { get; set; }
    public Guid StudyTypeId { get; set; }
    public Guid FacultyId { get; set; }
}