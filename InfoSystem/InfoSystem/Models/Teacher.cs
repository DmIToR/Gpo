namespace InfoSystem.Models;

public class Teacher : Profile
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string Job { get; set; }
}