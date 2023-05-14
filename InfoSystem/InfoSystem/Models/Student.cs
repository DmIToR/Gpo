using InfoSystem.Entities;

namespace InfoSystem.Models;

public class Student : Profile
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
}