using InfoSystem.Entities;

namespace InfoSystem.Models;

public class StudentProfile : Profile
{
    public StudentProfile(Guid id, string name, string surname, string patronymic, string group) 
        : base(id, name, surname, patronymic)
    {
        Group = group;
    }

    public string Group { get; set; }
}