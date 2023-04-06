using System.Security.Claims;
using InfoSystem.Entities;
using InfoSystem.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace InfoSystem.Controllers;


[ApiController]
[Route("[controller]")]
public class AccountController : Controller
{
    private readonly UserManager<User> _userManager; 
    private readonly SignInManager<User> _signInManager;

    public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    [HttpGet, Route("Test/GetUsers")]
    public List<User> GetUsers()
        => _userManager.Users.ToList();

    [HttpPost, Route("Test/CreateAdmin")]
    public async Task<IEnumerable<IdentityError>> CreateAdmin()
    {
        if (_userManager is null)
            throw new Exception();
        
        var user = new User
        {
            UserName = "admin"
        };

        var result = await _userManager.CreateAsync(user, "A1dm3in!");
        if (result.Succeeded)
            await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, "Admin"));

        return result.Errors;
    }

    [HttpPost, Route("SignIn")]
    public async Task<bool> SignIn(SignInViewModel model)
    {
        if (!ModelState.IsValid)
            return false;

        var user = await _userManager.FindByNameAsync(model.UserName);
        if (user is null)
        {
            ModelState.AddModelError("", "User not found.");
            return false;
        }

        var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);
        return result.Succeeded;
    }
}