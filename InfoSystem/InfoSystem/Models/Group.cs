using System.ComponentModel.DataAnnotations;

namespace InfoSystem.Models;

public class Group
{
    [Key] public Guid GroupId { get; set; }
    public string Name { get; set; }
    public DateTime RecruitmentYear { get; set; }
}