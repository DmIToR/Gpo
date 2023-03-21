namespace InfoSystem.Models;

public class Teacher
{
    public Teacher(Guid id, string name, string surname, string patronymic, string post, 
        string educationLevel, string academicDegree, string workExperience)
    {
        Id = id;
        Name = name;
        Surname = surname;
        Patronymic = patronymic;
        Post = post;
        EducationLevel = educationLevel;
        AcademicDegree = academicDegree;
        WorkExperience = workExperience;
    }

    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Patronymic { get; set; }
    public string Post { get; set; }
    public string EducationLevel { get; set; }
    public string AcademicDegree { get; set; }
    public string WorkExperience { get; set; }
}