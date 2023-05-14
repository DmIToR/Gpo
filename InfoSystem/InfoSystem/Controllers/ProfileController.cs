using InfoSystem.Data;
using InfoSystem.Entities;
using InfoSystem.Models;
using InfoSystem.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InfoSystem.Controllers;

[ApiController, Route("[controller]")]
public class ProfileController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<User> _userManager;

    public ProfileController(ApplicationDbContext context, UserManager<User> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    [HttpGet, Route("{id}")]
    public async Task<object> GetProfile(string id)
    {
        if (!ModelState.IsValid)
        {
            Response.StatusCode = StatusCodes.Status400BadRequest;
            return new { ErrorMessage = "Неверная структура данных." };
        }
        
        // var user = await _userManager.FindByNameAsync(username);
        var user = await _userManager.FindByIdAsync(id);
        if (user is null)
        {
            Response.StatusCode = StatusCodes.Status404NotFound;
            return new { ErrorMessage = "Пользователь не найден."};
        }
    
        Profile? profile;
        
        var claims = await _userManager.GetClaimsAsync(user);
        if (claims.Any(c => c.Value == "Student"))
        {
            profile = await _context.Students.FirstOrDefaultAsync(p => p.Id == user.Id);
            var studentGroup = await _context.StudentGroups.FirstOrDefaultAsync(e => 
                e.StudentId == ((Student)profile).Id 
                && e.IsCurrent);
            var group = await _context.Groups.FirstOrDefaultAsync(e => e.Id == studentGroup.GroupId);
            
            if (profile is not null) 
                return new
                {
                    Profile = new 
                    {
                        Username = user.UserName,
                        Name = user.Name,
                        Surname = user.Surname,
                        Patronymic = user.Patronymic,
                        Email = user.Email,
                        Group = group.Name
                    },
                    Role = "Студент"
                };
        }
    
        Response.StatusCode = StatusCodes.Status404NotFound;
        return new { ErrorMessage = "Профиль не найден." };
    }
}