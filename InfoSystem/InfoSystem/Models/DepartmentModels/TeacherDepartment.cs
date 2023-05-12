namespace InfoSystem.Models.DepartmentModels;

public class TeacherDepartment
{
    public Guid Id { get; set; }
    public Guid TeacherId { get; set; }
    public Guid DepartmentId { get; set; }
}