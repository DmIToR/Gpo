namespace InfoSystem.Models.CompanyModels;

public class ContactPerson
{
    public Guid Id { get; set; }
    public Guid CompanyId { get; set; }
    
    public string Name { get; set; }
    public string LastName { get; set; }
    public string Patronymic { get; set; }
    public string Job { get; set; }
    
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    
    public bool Head { get; set; }
    public bool Signatory { get; set; }
}