namespace InfoSystem.Models.DepartmentModels;

public class Department
{
    public Guid Id { get; set; }
    public Guid FacultyId { get; set; }
    
    public string Name { get; set; }
}