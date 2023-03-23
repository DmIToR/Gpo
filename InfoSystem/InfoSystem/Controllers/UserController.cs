using InfoSystem.Context;
using InfoSystem.Models;
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
    public Result AddUser(string username, string password)
    {
        var user = new User
        {
            UserId = Guid.NewGuid(),
            Login = username,
            Password = password
        };

        if (GetUser(username, password) is not null)
        {
            return new Result(false, "Такой пользователь уже существует");
        }

        _context.Add(user);
        _context.SaveChanges();

        return new Result(true, "Пользователь добавлен");
    }

    [HttpGet]
    [Route("AuthorizeUser")]
    public Result AuthorizeUser(string username, string password)
    {
        var user = GetUser(username, password);

        return user is not null ? new Result(true, $"Пользователь {user.Login} авторизовался") 
            : new Result(false, "Неправильно введены данные");
    }

    private User? GetUser(string username, string password)
    {
        return _context.Users
            .FirstOrDefault(user => user.Login == username && user.Password == password);
    }
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