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
    public Result AddUser(User user)
    {
        user.CreateGuid();

        if (GetUser(user.Login, user.Password) is not null)
        {
            return new Result(false, "Такое пользователь существует");
        }

        _context.Users.Add(user);
        _context.SaveChanges();

        return new Result(true, "Пользователь добавлен");
    }

    [HttpGet]
    [Route("AuthorizeUser")]
    public Result AuthorizeUser(string username, string password)
    {
        var user = GetUser(username, password);

        return user is not null ? new Result(true, $"Пользователь {user.Login} авторизовался") 
            : new Result(false, "Неправильно введыны данные");
    }
    
    [HttpPost]
    [Route("RemoveUsers")]
    public void RemoveUsers()
    {
        _context.Users.ToList().Clear();
        _context.SaveChanges();
    }

    private User? GetUser(string username, string password) 
        => _context.Users
            .FirstOrDefault(user => user.Login == username && user.Password == password);
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