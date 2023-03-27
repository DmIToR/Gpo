using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InfoSystem.Models;

public class AcademicPlan
{
    [Key] public Guid AcademicPlanId { get; set; }
    public string EducationalProgram { get; set; }
    public string Plan { get; set; }
    public DateTime RecruitmentYear { get; set; }
    public string EducationForm { get; set; }
    public string TrainingPeriod { get; set; }
}