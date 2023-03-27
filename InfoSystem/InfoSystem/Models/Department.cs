using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InfoSystem.Models;

public class Department
{
    [Key] public Guid Id { get; set; }
    public string Abbreviation { get; set; }
    public string Name { get; set; }
    public string DepartmentHead { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
}