using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InfoSystem.Models;

public class Student
{
    [Key, ForeignKey(nameof(User))] public Guid Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string? Patronymic { get; set; }
    public string Birthday { get; set; }
    public string RecordBook { get; set; }
    [ForeignKey(nameof(Group))] public Guid GroupId { get; set; }
}