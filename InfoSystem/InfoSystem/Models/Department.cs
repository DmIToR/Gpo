namespace InfoSystem.Models;

public class Department
{
    public Department(Guid id, string abbreviation, string name, string departmentHead, 
        string phone, string email, Guid facultyId)
    {
        Id = id;
        Abbreviation = abbreviation;
        Name = name;
        DepartmentHead = departmentHead;
        Phone = phone;
        Email = email;
        FacultyId = facultyId;
    }

    public Guid Id { get; set; }
    public string Abbreviation { get; set; }
    public string Name { get; set; }
    public string DepartmentHead { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public Guid FacultyId { get; set; }
}