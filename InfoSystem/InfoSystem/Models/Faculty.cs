using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InfoSystem.Models;

public class Faculty
{
    [Key] public Guid Id { get; set; }
    public string Abbreviation { get; set; }
    public string Name { get; set; }
    public string Dukan { get; set; }
    public string Address { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    [ForeignKey(nameof(AcademicPlan))] public Guid AcademicPlanId { get; set; }
    [ForeignKey(nameof(Department))] public Guid DepartmentId { get; set; }
}