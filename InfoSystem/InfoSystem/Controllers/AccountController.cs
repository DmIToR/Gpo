using System.Net;
using System.Security.Claims;
using InfoSystem.Data;
using InfoSystem.Entities;
using InfoSystem.Models.CompanyModels;
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
    private readonly ApplicationDbContext _context;

    public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, IConfiguration configuration, ApplicationDbContext context)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _configuration = configuration;
        _context = context;
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

    [HttpPost, Route("Tools/CreateDocumentInfo")]
    public async Task<object> CreateDocumentInfo(string cityName, string companyName, int itn, string companyShortName,
        string companyAddress, string companyPhoneNumber, string companyEmail, string name, string lastName, 
        string patronymic, string number, string email, string job, bool isHead, bool isSignatory)
    {
        var city = _context.Cities
            .Select(f => f)
            .FirstOrDefault(f => f.Name == name);

        if (city != null)
        {
            Response.StatusCode = StatusCodes.Status409Conflict;
            return new
            {
                Message = $"Город {name} уже есть в базе данных"
            };
        }

        _context.Cities.Add(new City()
        {
            Name = name
        });
        
        await _context.SaveChangesAsync();

        var company = _context.Companies
            .Select(f => f)
            .FirstOrDefault(f => f.Name == companyName);

        if (company != null)
        {
            Response.StatusCode = StatusCodes.Status409Conflict;
            return new
            {
                Message = $"Такая компания {name} уже есть в базе данных"
            };
        }
        
        city = _context.Cities
            .Select(f => f)
            .FirstOrDefault(f => f.Name == name);

        _context.Companies.Add(new Company()
        {
            CityId = city.Id,
            Name = companyName,
            Itn = itn,
            ShortName = companyShortName,
            Address = companyAddress,
            PhoneNumber = companyPhoneNumber,
            Email = companyEmail
        });
        
        await _context.SaveChangesAsync();

        var contactPerson = _context.ContactPersons
            .Select(f => f)
            .FirstOrDefault(f => f.PhoneNumber == number && f.Email == email);

        if (contactPerson != null)
        {
            Response.StatusCode = StatusCodes.Status409Conflict;
            return new
            {
                Message = $"Такое контактное лицо {name} уже есть в базе данных"
            };
        }
        
        company = _context.Companies
            .Select(f => f)
            .FirstOrDefault(f => f.Name == companyName);

        _context.ContactPersons.Add(new ContactPerson()
        {
            CompanyId = company.Id,
            Name = name,
            LastName = lastName,
            Patronymic = patronymic,
            PhoneNumber = number,
            Email = email,
            Job = job,
            Head = isHead,
            Signatory = isSignatory,
        });
        
        await _context.SaveChangesAsync();
        Response.StatusCode = StatusCodes.Status200OK;
        return new
        {
            Message = $"Данные для догвоора успешно заполнены"
        };
    }
}