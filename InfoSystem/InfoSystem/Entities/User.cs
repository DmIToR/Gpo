using Microsoft.AspNetCore.Identity;

namespace InfoSystem.Entities;

public class User : IdentityUser<Guid>
{
    public string? Name { set; get; }
    public string? Surname { set; get; }
    public string? Patronymic { set; get; }
}