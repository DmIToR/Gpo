using System.Security.Claims;
using InfoSystem.Data;
using InfoSystem.Entities;
using InfoSystem.Models;
using InfoSystem.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace InfoSystem.Controllers;

// [Authorize(Policy = "Admin")]
[ApiController]
[Route("[controller]")]
public class AdminController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<User> _userManager; 

    public AdminController(ApplicationDbContext context, UserManager<User> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    [HttpPost, Route("Tools/CreateUser")]
    public async Task<object> CreateUser(SignUpViewModel model)
    {
        var k = model.Role.ToString();

        var isNumber = int.TryParse(k, out var res);

        if (isNumber)
        {
            Response.StatusCode = StatusCodes.Status400BadRequest;
            return new { ErrorMessage = "Не существует такой роли" };
        }
        
        if (!ModelState.IsValid)
        {
            Response.StatusCode = StatusCodes.Status400BadRequest;
            return new { ErrorMessage = "Неверная структура данных." };
        }

        if (await _userManager.FindByNameAsync(model.UserName) is not null
            || await _userManager.FindByEmailAsync(model.Email) is not null)
        {
            Response.StatusCode = StatusCodes.Status409Conflict;
            ModelState.AddModelError("", "User already exists.");
            return new { ErrorMessage = "Пользователь с такими данными уже существует." };
        }

        var user = new User
        {
            UserName = model.UserName,
            Email = model.Email
        };

        var result = await _userManager.CreateAsync(user, model.Password);
        if (!result.Succeeded)
        {
            Response.StatusCode = StatusCodes.Status400BadRequest;
            return new { ErrorMessage = result.Errors };
        }

        await _userManager.AddClaimAsync(
            user, 
            new Claim(ClaimTypes.Role, model.Role.ToString())
        );
        
        if (model.Role == UserRole.Student)
        {
            _context.Add(new StudentProfile { Id = user.Id });
        }
        
        if (model.Role == UserRole.Teacher)
        {
            _context.Add(new TeacherProfile { Id = user.Id });
        }
        
        if (model.Role == UserRole.Secretary)
        {
            _context.Add(new SecretaryProfile { Id = user.Id });
        }
        
        if (model.Role == UserRole.EducationDepartment)
        {
            _context.Add(new EducationDepartmentProfile { Id = user.Id });
        }

        await _context.SaveChangesAsync();

        return new { Message = $"Пользователь {model.UserName} успешно создан." };
    }
    
    [HttpDelete, Route("Tools/DeleteUser")]
    public async Task<bool> DeleteUser(DeleteUserViewModel model)
    {
        if (!ModelState.IsValid)
            return false;

        var user = await _userManager.FindByNameAsync(model.UserName);
        if (user is null)
        {
            ModelState.AddModelError("", "User does not exist.");
            return false;
        }

        var result = await _userManager.DeleteAsync(user);
        return result.Succeeded;
    }

    [HttpPatch, Route("Tools/ChangeUserPassword")]
    public async Task<object> ChangeUserPassword(ChangeUserPasswordViewModel model)
    {
        if (!ModelState.IsValid)
        {
            Response.StatusCode = StatusCodes.Status400BadRequest;
            return new { ErrorMessage = "Неверная структура данных." };
        }
        
        var user = await _userManager.FindByNameAsync(model.UserName);
        if (user is null)
        {
            Response.StatusCode = StatusCodes.Status404NotFound;
            return new { ErrorMessage = "Пользователь не найден."};
        }

        var validator = new PasswordValidator<User>();
        var validResult = await validator.ValidateAsync(_userManager, null, model.Password);

        if (!validResult.Succeeded)
        {
            Response.StatusCode = StatusCodes.Status400BadRequest;
            return new { ErrorMessage = validResult.Errors };
        }

        var removeResult = await _userManager.RemovePasswordAsync(user);
        if (!removeResult.Succeeded)
        {
            Response.StatusCode = StatusCodes.Status500InternalServerError;
            return new { ErrorMessage = "Не удалось удалить пароль." };
        }

        await _userManager.AddPasswordAsync(user, model.Password);
        return new { Message = $"Пароль для пользователя {user.UserName} успешно изменен." };
    }
    
    [HttpGet, Route("Tools/GetUsers")]
    public List<User> GetUsers()
        => _userManager.Users.ToList();
}