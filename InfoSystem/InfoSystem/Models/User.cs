using System.ComponentModel.DataAnnotations;

namespace InfoSystem.Models;

public class User
{
    [Key] public Guid UserId { get; private set; }
    public string Login { get; set; }
    public string Password { get; set; }
    public string SecretCode { get; set; }

    public User(string login, string password)
    {
        Login = login;
        Password = password;
    }

    public void CreateGuid()
    {
        UserId = Guid.NewGuid();
    }
}