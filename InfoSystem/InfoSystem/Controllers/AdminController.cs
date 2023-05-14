using System.Security.Claims;
using InfoSystem.Data;
using InfoSystem.Entities;
using InfoSystem.Models;
using InfoSystem.Models.CompanyModels;
using InfoSystem.Models.DepartmentModels;
using InfoSystem.Models.PracticeModels;
using InfoSystem.Models.StudyPlanModels;
using InfoSystem.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Novacode;

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

        if (model.Role.ToString() == "Student")
        {
            var faculty = new Faculty
            {
                Name = "Факультет систем управления"
            };
            
            _context.Add(faculty);
            await _context.SaveChangesAsync();

            var studyType = new StudyType
            {
                StudyTypeName = "Очный"
            };
                
            _context.Add(studyType);
            await _context.SaveChangesAsync();

            var studyProgram = new StudyProgram
            {
                Name = "Бакалавриат"
            };
                
            _context.Add(studyProgram);
            await _context.SaveChangesAsync();

            var course = new Course
            {
                Name = "1 курс",
                StudyProgramId = studyProgram.Id
            };
            
            _context.Add(course);
            await _context.SaveChangesAsync();

            var department = new Department
            {
                FacultyId = faculty.Id,
                Name = "Автоматизированных систем упрвления"
            };
            
            _context.Add(department);
            await _context.SaveChangesAsync();

            var studyPlan = new StudyPlan
            {
                CourseId = course.Id,
                StudyTypeId = studyType.Id,// тут по идее факульти id(хз)
                FacultyId = studyType.Id,
            };
            
            _context.Add(studyPlan);
            await _context.SaveChangesAsync();

            var group = new Group
            {
                Name = "430-4",
                StudyPlanId = studyPlan.Id,
                DepartmentId = department.Id
            };

            _context.Add(group);
            await _context.SaveChangesAsync();
            _context.ChangeTracker.Clear();

            var student = new Student
            {
                Id = user.Id,
                UserId = user.Id
            };

            _context.Add(student);
            await _context.SaveChangesAsync();
            
            _context.Add(new StudentGroup
            {
                Id = group.Id,
                GroupId = group.Id,
                StudentId = student.Id
            });
            
            await _context.SaveChangesAsync();
            
            return new { Message = $"Пользователь {model.UserName} успешно создан(студент)." };
        }

        if (model.Role.ToString() == "Teacher")
        {
            var teacher = _context.Add(new Teacher()
            {
                UserId = user.Id
            }); // доделать учителя
            
            return new { Message = $"Пользователь {model.UserName} успешно создан(учитель)." };
        }

        _context.ChangeTracker.Clear();
        await _context.SaveChangesAsync();

        return new { Message = $"Пользователь {model.UserName} успешно создан." };
    }
    
    [HttpDelete, Route("Tools/DeleteUser/{userName}")]
    public async Task<object> DeleteUser(string userName)
    {
        if (!ModelState.IsValid)
        {
            Response.StatusCode = StatusCodes.Status400BadRequest;
            return new { ErrorMessage = "Неверная структура данных." };
        }

        var user = await _userManager.FindByNameAsync(userName);
        if (user is null)
        {
            Response.StatusCode = StatusCodes.Status409Conflict;
            // ModelState.AddModelError("", "User already exists.");
            return new { ErrorMessage = "Пользователь с такими данными не существует." };
        }

        var result = await _userManager.DeleteAsync(user);
        if (!result.Succeeded)
        {
            Response.StatusCode = StatusCodes.Status400BadRequest;
            return new { ErrorMessage = result.Errors };
        }
            
        return new { Message = $"Пользователь {userName} успешно удалён." };
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
    
    [HttpGet, Route("Tools/GetUserByUsername/{userName}")]
    public async Task<object> GetUserByUsername(string userName)
    {
        // var userById = await _userManager.FindByIdAsync(id);
        var user = await _userManager.FindByNameAsync(userName);
        if (user is null)
        {
            Response.StatusCode = StatusCodes.Status404NotFound;
            return new { ErrorMessage = "Пользователь не найден."};
        }
        
        var role = _userManager.GetClaimsAsync(user).Result.First().Value;
        if (role == "Student")
        {
            var student = _context.Students
                .Select(s => s)
                .First(s => s.Id.ToString() == user.Id.ToString());

            var groupId = _context.StudentGroups
                .Select(s => s)
                .First(s => s.StudentId.ToString() == student.Id.ToString());
            
            var group = _context.Groups
                .Select(s => s)
                .First(s => s.Id.ToString() == groupId.Id.ToString());
            
            var studyPlan = _context.StudyPlans
                .Select(s => s)
                .First(s => s.Id.ToString() == group.StudyPlanId.ToString());
            
            var department = _context.Departments
                .Select(s => s)
                .First(s => s.Id.ToString() == group.DepartmentId.ToString());
            
            var course = _context.Courses
                .Select(s => s)
                .First(s => s.Id.ToString() == studyPlan.CourseId.ToString());
            
            var studyProgram = _context.StudyPrograms
                .Select(s => s)
                .First(s => s.Id.ToString() == course.StudyProgramId.ToString());
            
            var faculty = _context.Faculties
                .Select(s => s)
                .First(s => s.Id.ToString() == department.FacultyId.ToString());
            
            var studyType = _context.StudyTypes
                .Select(s => s)
                .First(s => s.Id.ToString() == studyPlan.StudyTypeId.ToString());

            Response.StatusCode = StatusCodes.Status200OK;
            return new
            {
                user = role,
                groupName = group.Name,
                departmentName = department.Name,
                courseName = course.Name,
                studyProgramName = studyProgram.Name,
                facultyName = faculty.Name,
                studyType.StudyTypeName
            };
        }
        
        return new
        {
            user = role
        };
    }
    
    [HttpGet, Route("Tools/GetUsers")]
    public List<User> GetUsers()
        => _userManager.Users.ToList();

    [HttpPost, Route("Tools/CreateDocument")]
    public async Task<object> CreateDocument(string id)
    {
        var city = new City()
        {
            Name = "Москва"
        };
        
        _context.Add(city);
        await _context.SaveChangesAsync();
        
        var company = new Company()
        {
            CityId = city.Id,
            Address = "Ул.Есенина д.6",
            Email = "vk@mail.ru",
            Itn = 55,
            Name = "Общество с ограниченной ответственностью «ВКонтакте»",
            ShortName = "ООО «ВК»"
        };
            
        _context.Add(company);
        await _context.SaveChangesAsync();
        
        var signatory = new Signatory()
        {
            Name = "В.",
            LastName = "Степанов",
            Patronymic = "С.",
            Job = "генеральный директор"
        };
            
        _context.Add(signatory);
        await _context.SaveChangesAsync();
        
        var contact = new ContactPerson()
        {       
            Name = "Василий",
            LastName = "Степанов",
            Patronymic = "Сергеевич",
            PhoneNumber = "8-931-312-41-41",
            CompanyId = company.Id,
            Email = "stepanovVK@mail.ru",
            Head = true,
            Signatory = true
        };
            
        _context.Add(contact);
        await _context.SaveChangesAsync();
        
        var practiceKind = new PracticeKind()
        {
            Name = "Производственная практика: "
        };
            
        _context.Add(practiceKind);
        await _context.SaveChangesAsync();
        
        var practiceType = new PracticeType()
        {
            PracticeKindId = practiceKind.Id,
            Name = "Научно-исследовательская работа"
        };
            
        _context.Add(practiceType);
        await _context.SaveChangesAsync();
        
        var studentGroup = _context.StudentGroups
            .Select(s => s)
            .First(s => s.StudentId.ToString() == id);
        
        var group = _context.Groups
            .Select(s => s)
            .First(s => s.Id.ToString() == studentGroup.GroupId.ToString());
        
        var practice = new Practice()
        {
            StudyPlanId = group.StudyPlanId,
            PracticeTypeId = practiceType.Id,
            Semester = 6
        };
        
        _context.Add(practice);
        await _context.SaveChangesAsync();
        
        var authority = new Authority()
        {
             SignatoryId = signatory.Id,
             Number = "55",
             Start = DateTime.Now
        };
            
        _context.Add(authority);
        await _context.SaveChangesAsync();

        var practicePeriod = new PracticePeriod()
        {
            PracticeId = practice.Id,
            PracticeStart = DateTime.Now,
            PracticeEnd = new DateTime(2023, 08, 22), 
        };
            
        _context.Add(practicePeriod);
        await _context.SaveChangesAsync();
        
        var contract = new Contract()
        {
            CompanyId = company.Id,
            SignatoryId = signatory.Id,
            ContactPersonId = contact.Id,
            Number = 5,
            Date = DateTime.Now
        };
            
        _context.Add(contract);
        await _context.SaveChangesAsync();
        
        var practiceContract = new PracticeContract()
        {
            PracticeId = practice.Id,
            ContractId = contract.Id,
        };
            
        _context.Add(practiceContract);
        await _context.SaveChangesAsync();
        
        // string templatePath = @"C:\Users\roman\Gpo\InfoSystem\InfoSystem\Dog.docx";
        //
        // string variable1Name = "ЗАЛУПА";
        // string variable1Value = "Хуй";
        //
        // DocX doc = DocX.Load(templatePath);
        //
        // doc.ReplaceText(variable1Name, variable1Value);
        //
        // doc.SaveAs(templatePath);
        // doc.Dispose();

        return new { Message = group.StudyPlanId };
    }
}

