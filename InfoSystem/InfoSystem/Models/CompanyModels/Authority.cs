namespace InfoSystem.Models.CompanyModels;

public class Authority
{
    public Guid Id { get; set; }
    
    public Guid SignatoryId { get; set; }
    
    public DateTime Start { get; set; }
    public string Number { get; set; }
}