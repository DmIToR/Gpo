using InfoSystem.Entities;
using InfoSystem.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace InfoSystem.Controllers;

[Authorize(Policy = "Admin")]
[ApiController]
[Route("[controller]")]
public class AdminController : Controller
{
    private readonly UserManager<User> _userManager; 

    public AdminController(UserManager<User> userManager, SignInManager<User> signInManager)
    {
        _userManager = userManager;
    }

    [HttpPost, Route("Tools/CreateUser")]
    public async Task<bool> CreateUser(SignUpViewModel model)
    {
        if (!ModelState.IsValid)
            return false;

        if (await _userManager.FindByNameAsync(model.UserName) is not null
            && await _userManager.FindByEmailAsync(model.Email) is not null)
        {
            ModelState.AddModelError("", "User already exists.");
            return false;
        }

        var user = new User
        {
            UserName = model.UserName,
            Email = model.Email
        };

        var result = await _userManager.CreateAsync(user, model.Password);
        return result.Succeeded;
    }
}