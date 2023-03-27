using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InfoSystem.Models;

public class Teacher
{
    [Key, ForeignKey(nameof(User))] public Guid TeacherId { get; set; }
    public string Patronymic { get; set; }
    public string Post { get; set; }
    public string EducationLevel { get; set; }
    public string AcademicDegree { get; set; }
    public string WorkExperience { get; set; }
}