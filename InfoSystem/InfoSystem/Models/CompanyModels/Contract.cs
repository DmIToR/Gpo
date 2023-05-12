namespace InfoSystem.Models.CompanyModels;

public class Contract
{
    public Guid Id { get; set; }
    
    public Guid TeacherId { get; set; }
    public Guid SignatoryId { get; set; }
    public Guid ContactPersonId { get; set; }
    public Guid CompanyId { get; set; }
    
    public int Number { get; set; }
    public DateTime Date { get; set; }
}