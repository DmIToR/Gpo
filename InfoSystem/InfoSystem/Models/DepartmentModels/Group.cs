namespace InfoSystem.Models.DepartmentModels;

public class Group
{
    public Guid Id { get; set; }
    public Guid DepartmentId { get; set; }
    public Guid StudyPlanId { get; set; }
    
    public string Name { get; set; }
}