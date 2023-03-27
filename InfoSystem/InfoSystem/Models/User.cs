using System.ComponentModel.DataAnnotations;

namespace InfoSystem.Models;

public class User
{
    [Key] public Guid UserId { get; private set; }
    public string Login { get; set; }
    public string Password { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string SecretPhrase { get; set; }

    public void CreateGuid()
    {
        UserId = Guid.NewGuid();
    }
}