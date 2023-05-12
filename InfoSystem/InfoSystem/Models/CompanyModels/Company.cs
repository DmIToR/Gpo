namespace InfoSystem.Models.CompanyModels;

public class Company
{
    public Guid Id { get; set; }
    public Guid CityId { get; set; }
    public long Itn { get; set; }
    public string Name { get; set; }
    public string ShortName { get; set; }
    public string Address { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
}