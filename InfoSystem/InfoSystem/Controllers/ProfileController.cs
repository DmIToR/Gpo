using InfoSystem.Data;
using InfoSystem.Entities;
using InfoSystem.Models;
using InfoSystem.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InfoSystem.Controllers;

[Authorize, ApiController, Route("[controller]")]
public class ProfileController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<User> _userManager;

    public ProfileController(ApplicationDbContext context, UserManager<User> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    [HttpGet, Route("{username}")]
    public async Task<object> GetProfile(string username)
    {
        if (!ModelState.IsValid)
        {
            Response.StatusCode = StatusCodes.Status400BadRequest;
            return new { ErrorMessage = "Неверная структура данных." };
        }
        
        var user = await _userManager.FindByNameAsync(username);
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
            
            if (profile is not null) 
                return profile;
        }

        Response.StatusCode = StatusCodes.Status404NotFound;
        return new { ErrorMessage = "Профиль не найден." };
    }

    // [HttpPatch, Route("{username}/Edit")]
    // public async Task<object> EditProfile(EditStudentProfileViewModel model, string username)
    // {
    //     if (!ModelState.IsValid)
    //     {
    //         Response.StatusCode = StatusCodes.Status400BadRequest;
    //         return new { ErrorMessage = "Неверная структура данных." };
    //     }
    //     
    //     var user = await _userManager.FindByNameAsync(username);
    //     if (user is null)
    //     {
    //         Response.StatusCode = StatusCodes.Status404NotFound;
    //         return new { ErrorMessage = "Пользователь не найден."};
    //     }
    //     
    //     
    // }
}