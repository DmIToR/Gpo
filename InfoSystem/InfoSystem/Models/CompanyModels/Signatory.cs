namespace InfoSystem.Models.CompanyModels;

public class Signatory
{
    public Guid Id { get; set; }

    public string Name { get; set; }
    public string LastName { get; set; }
    public string Patronymic { get; set; }
    
    public string Job { get; set; }
}