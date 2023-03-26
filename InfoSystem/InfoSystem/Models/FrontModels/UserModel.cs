namespace InfoSystem.Models.FrontModels;

public class UserModel
{
    public string Login { get; set; }
    public string Password { get; set; }
    public string SecretCode { get; set; }

    public UserModel(string login, string password, string secretCode)
    {
        Login = login;
        Password = password;
        SecretCode = secretCode;
    }
}