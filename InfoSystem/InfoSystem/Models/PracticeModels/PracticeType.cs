namespace InfoSystem.Models.PracticeModels;

public class PracticeType
{
    public Guid Id { get; set; }
    public Guid PracticeKindId { get; set; }
    
    public string Name { get; set; }
}