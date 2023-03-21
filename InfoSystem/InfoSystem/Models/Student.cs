namespace InfoSystem.Models;

public class Student
{
    public Student(Guid id, string name, string surname, string? patronymic, 
        string birthday, string recordBook, Guid groupId)
    {
        Id = id;
        Name = name;
        Surname = surname;
        Patronymic = patronymic;
        Birthday = birthday;
        RecordBook = recordBook;
        GroupId = groupId;
    }
    
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string? Patronymic { get; set; }
    public string Birthday { get; set; }
    public string RecordBook { get; set; }
    public Guid GroupId { get; set; }
}