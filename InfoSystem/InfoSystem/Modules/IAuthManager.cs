using InfoSystem.Entities;

namespace InfoSystem.Modules;

public interface IAuthManager
{
    string CreateToken(User user);
}