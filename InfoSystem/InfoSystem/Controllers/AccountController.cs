using System.Net;
using System.Security.Claims;
using InfoSystem.Entities;
using InfoSystem.Modules;
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
    private readonly IConfiguration _configuration;

    public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, IConfiguration configuration)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _configuration = configuration;
    }

    // [HttpPost, Route("Test/CreateAdmin")]
    // public async Task<IEnumerable<IdentityError>> CreateAdmin()
    // {
    //     if (_userManager is null)
    //         throw new Exception();
    //     
    //     var user = new User
    //     {
    //         UserName = "admin"
    //     };
    //
    //     var result = await _userManager.CreateAsync(user, "A1dm3in!");
    //     if (result.Succeeded)
    //         await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, "Admin"));
    //
    //     return result.Errors;
    // }

    [HttpPost, Route("SignIn")]
    public async Task<object> SignIn(SignInViewModel model)
    {
        if (!ModelState.IsValid)
        {
            Response.StatusCode = StatusCodes.Status400BadRequest;
            return new { ErrorMessage = "Неверные данные." };
        }

        var user = await _userManager.FindByNameAsync(model.UserName);
        if (user is null)
        {
            Response.StatusCode = StatusCodes.Status404NotFound;
            return new { ErrorMessage = "Пользователь не найден."};
        }

        var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);

        var authManager = new AuthManager(_configuration.GetValue<string>("Jwt:SecretKey"));

        if (result.Succeeded)
        {
            return new
            {
                Id = user.Id, 
                AuthToken = authManager.CreateToken(user)
            };
        }
        
        Response.StatusCode = StatusCodes.Status401Unauthorized;
        return new { ErrorMessage = "Неверный пароль." };
    }
}