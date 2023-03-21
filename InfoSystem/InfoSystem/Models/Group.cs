namespace InfoSystem.Models;

public class Group
{
    public Group(Guid id, string name, DateTime recruitmentYear)
    {
        Id = id;
        Name = name;
        RecruitmentYear = recruitmentYear;
    }

    public Guid Id { get; set; }
    public string Name { get; set; }
    public DateTime RecruitmentYear { get; set; }
}