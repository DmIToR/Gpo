namespace InfoSystem.Models.PracticeModels;

public class Practice
{
    public Guid Id { get; set; }
    
    public Guid StudyPlanId { get; set; }
    public Guid PracticeTypeId { get; set; }

    public int Semester { get; set; }
}