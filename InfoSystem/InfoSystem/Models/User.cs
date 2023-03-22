using System.ComponentModel.DataAnnotations;

namespace InfoSystem.Models;

public class User
{
    [Key] public Guid UserId { get; set; }
    public string Login { get; set; }
    public string Password { get; set; }
}