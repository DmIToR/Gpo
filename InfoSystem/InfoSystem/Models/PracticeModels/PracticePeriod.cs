namespace InfoSystem.Models.PracticeModels;

public class PracticePeriod
{
    public Guid Id { get; set; }
    public Guid PracticeId { get; set; }
    
    public DateTime PracticeStart { get; set; }
    public DateTime PracticeEnd { get; set; }
}