using InfoSystem.Context;
using InfoSystem.Models;
using InfoSystem.Models.FrontModels;
using Microsoft.AspNetCore.Mvc;

namespace InfoSystem.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : Controller
{
    private readonly DataContext _context;

    public UserController(DataContext context)
    {
        _context = context;
    }

    [HttpGet]
    [Route("GetUsers")]
    public List<User> GetUsers()
        => _context.Users.ToList();

    [HttpPost]
    [Route("AddUser")]
    public Result AddUser(User user, string secretCode)
    {
        if (GetUser(user.Login) is not null)
        {
            return GetResult(404, false, "Такой пользователь существует");
        }
        
        user.CreateGuid();

        var userModel = new UserModel(user.Login, user.Password, secretCode); // add in bd
        user.SecretCode = secretCode;
        _context.Users.Add(user);
        _context.SaveChanges();
        
        return GetResult(200, true, "Пользователь добавлен");
    }

    [HttpGet]
    [Route("AuthorizeUser")]
    public Result AuthorizeUser(string username, string password)
    {
        var user = GetUser(username);

        return user is not null 
            ? GetResult(200, true, $"Пользователь {user.Login} авторизовался")
            : GetResult(404, false, "Неправильно введены данные");
    }

    [HttpPost]
    [Route("RemoveUsers")]
    public void RemoveUsers()
    {
        var users = _context.Users.ToList();
        
        _context.Users.RemoveRange(users);
        _context.SaveChanges();
    }

    [HttpPost]
    [Route("PasswordRecovery")]
    public Result PasswordRecovery(string username)
    {
        if (GetUser(username) is not null)
        {
            var password = GeneratePassword();

            for (int i = 0; i < _context.Users.ToList().Count; i++)
            {
                if (_context.Users.ToList()[i].Login == username)
                {
                    _context.Users.ToList()[i].Password = password;
                }
            }

            _context.SaveChanges();
            return GetResult(200, true, $"Новый пароль: {password}");
        }
        
        return GetResult(404, false,"Пользователь не найден");
    }
    
    [HttpPost]
    [Route("EnterSecretCode")]
    public Result EnterSecretCode(string username, string code)
    {
        var user = GetUser(username);
        
        if (user is not null && user.SecretCode == code)
        {
            return GetResult(200, true, $"Секретный код введен правилньо");
        }
        
        return GetResult(404, false, $"Секретный код введен не верно");
    }
    
    [HttpPost]
    [Route("SetPassword")]
    public Result SetPassword(string username, string password)
    {
        for (int i = 0; i < _context.Users.ToList().Count; i++)
        {
            if (_context.Users.ToList()[i].Login == username)
            {
                _context.Users.ToList()[i].Password = password;
                _context.SaveChanges();
                return GetResult(200, true, $"Пароль изменен. Новый пароль: {password}");
            }
        }

        return GetResult(404, false, $"Такого юзера нету");
    }

    private Result GetResult(int statusCode, bool status, string resultMessage)
    {
        HttpContext.Response.StatusCode = statusCode;
        return new Result(status, resultMessage);
    }

    private string GeneratePassword()
        => "abc";

    private User? GetUser(string username)
        => _context.Users
            .FirstOrDefault(user => user.Login == username);
}

public struct Result
{
    public bool IsContains { get; }
    public string Message { get; }

    public Result(bool isContains, string message)
    {
        IsContains = isContains;
        Message = message;
    }
}