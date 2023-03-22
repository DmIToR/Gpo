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

    [HttpGet(Name = "GetUsers")]
    public List<User> GetUsers()
        => _context.Users.ToList();
}