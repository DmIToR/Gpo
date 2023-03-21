namespace InfoSystem.Models;

public class Faculty
{
    public Faculty(Guid id, string abbreviation, string name, string dukan, string address, 
        string phone, string email, Guid planId)
    {
        Id = id;
        Abbreviation = abbreviation;
        Name = name;
        Dukan = dukan;
        Address = address;
        Phone = phone;
        Email = email;
        PlanId = planId;
    }

    public Guid Id { get; set; }
    public string Abbreviation { get; set; }
    public string Name { get; set; }
    public string Dukan { get; set; }
    public string Address { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public Guid PlanId { get; set; }
}