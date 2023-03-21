namespace InfoSystem.Models;

public class AcademicPlan
{
    public AcademicPlan(Guid id, string educationalProgram, string plan, Guid facultyId, Guid departmentId, 
        DateTime recruitmentYear, string educationForm, string trainingPeriod)
    {
        Id = id;
        EducationalProgram = educationalProgram;
        Plan = plan;
        FacultyId = facultyId;
        DepartmentId = departmentId;
        RecruitmentYear = recruitmentYear;
        EducationForm = educationForm;
        TrainingPeriod = trainingPeriod;
    }

    public Guid Id { get; set; }
    public string EducationalProgram { get; set; }
    public string Plan { get; set; }
    public Guid FacultyId { get; set; }
    public Guid DepartmentId { get; set; }
    public DateTime RecruitmentYear { get; set; }
    public string EducationForm { get; set; }
    public string TrainingPeriod { get; set; }
}