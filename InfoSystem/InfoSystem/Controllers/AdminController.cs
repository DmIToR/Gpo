using System.Dynamic;
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

//[Authorize(Policy = "Admin")]
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

        await _context.SaveChangesAsync();

        return new { Message = $"Пользователь {model.UserName} успешно создан." };
    }

    [HttpPost, Route("Tools/CreateStudent")]
    public async Task<object> CreateStudent(SignUpViewModel model)
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

        var faculty = new Faculty
        {
            Name = "Факультет систем управления"
        };

        _context.Add(faculty);
        await _context.SaveChangesAsync();

        var studyType = new StudyType
        {
            StudyTypeName = "очное"
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
            StudyTypeId = studyType.Id, // тут по идее факульти id(хз)
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

    [HttpPost, Route("Tools/CreateTeacher")]
    public async Task<object> CreateTeacher(SignUpViewModel model, string jobName)
    {
        if (!ModelState.IsValid)
        {
            Response.StatusCode = StatusCodes.Status400BadRequest;
            return new { ErrorMessage = "Неверная структура данных." };
        }

        model.Role = UserRole.Teacher;

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

        var teacher = _context.Add(new Teacher()
        {
            UserId = user.Id,
            Job = "Docent"
        });

        _context.ChangeTracker.Clear();
        await _context.SaveChangesAsync();

        return new { Message = $"Пользователь {model.UserName} успешно создан(учитель)." };
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
            return new { ErrorMessage = "Пользователь не найден." };
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

    [HttpGet, Route("Tools/GetUserById")]
    public async Task<object> GetUserById(string id)
    {
        var userById = await _userManager.FindByIdAsync(id);
        if (userById is null)
        {
            Response.StatusCode = StatusCodes.Status404NotFound;
            return new { ErrorMessage = "Пользователь не найден." };
        }

        var role = _userManager.GetClaimsAsync(userById).Result.First().Value;
        if (role == "Student")
        {
            var student = _context.Students
                .Select(s => s)
                .FirstOrDefault(s => s.Id.ToString() == id);

            if (student != null)
            {
                var groupId = _context.StudentGroups
                    .Select(s => s)
                    .FirstOrDefault(s => s.StudentId.ToString() == student.Id.ToString());

                var group = _context.Groups
                    .Select(s => s)
                    .FirstOrDefault(s => s.Id.ToString() == groupId.Id.ToString());

                var studyPlan = _context.StudyPlans
                    .Select(s => s)
                    .FirstOrDefault(s => s.Id.ToString() == group.StudyPlanId.ToString());

                var department = _context.Departments
                    .Select(s => s)
                    .FirstOrDefault(s => s.Id.ToString() == group.DepartmentId.ToString());

                var course = _context.Courses
                    .Select(s => s)
                    .FirstOrDefault(s => s.Id.ToString() == studyPlan.CourseId.ToString());

                var studyProgram = _context.StudyPrograms
                    .Select(s => s)
                    .FirstOrDefault(s => s.Id.ToString() == course.StudyProgramId.ToString());

                var faculty = _context.Faculties
                    .Select(s => s)
                    .FirstOrDefault(s => s.Id.ToString() == department.FacultyId.ToString());

                var studyType = _context.StudyTypes
                    .Select(s => s)
                    .FirstOrDefault(s => s.Id.ToString() == studyPlan.StudyTypeId.ToString());
                
                Response.StatusCode = StatusCodes.Status200OK;

                var data = new
                {
                    id = userById.Id,
                    user = role,
                    name = userById.Name,
                    surname = userById.Surname,
                    email = userById.Email,
                    groupName = group?.Name,
                    departmentName = department?.Name,
                    courseName = course?.Name,
                    studyProgramName = studyProgram?.Name,
                    facultyName = faculty?.Name,
                    studyType?.StudyTypeName
                };
                
                return Json(data);
            }

            var d = new
            {
                id = userById.Id,
                user = role,
                name = userById.Name,
                surname = userById.Surname,
                email = userById.Email,
                groupName = "",
                departmentName = "",
                courseName = "",
                studyProgramName = "",
                facultyName = "",
                studyType = ""
            };
                
            return Json(d);
        }
        
        Response.StatusCode = StatusCodes.Status404NotFound;
        return new
        {
            Message = $"Такого пользователя нету в базе данных"
        };
    }

    [HttpPost, Route("Tools/EditUser")]
    public async Task<object> EditUser(StudentViewModel viewModel)
    {
        await _context.SaveChangesAsync();

        Response.StatusCode = StatusCodes.Status200OK;
        return new
        {
            Message = $"Данные пользователя успешно изменены"
        };
    }

    [HttpGet, Route("Tools/GetUsers")]
    public List<User> GetUsers()
        => _userManager.Users.ToList();

    // [HttpPost, Route("Tools/CreateDocument")]
    // public async Task<object> CreateDocument(string id)
    // {
    //     // string templatePath = @"C:\Users\roman\Gpo\InfoSystem\InfoSystem\Dog.docx";
    //     //
    //     // string variable1Name = "ЗАЛУПА";
    //     // string variable1Value = "Хуй";
    //     //
    //     // DocX doc = DocX.Load(templatePath);
    //     //
    //     // doc.ReplaceText(variable1Name, variable1Value);
    //     //
    //     // doc.SaveAs(templatePath);
    //     // doc.Dispose();
    //
    //     return new { Message = group.StudyPlanId };
    // }

    [HttpPost, Route("Tools/AddFaculty")]
    public async Task<object> AddFaculty(string name)
    {
        var faculty = _context.Faculties
            .Select(f => f)
            .FirstOrDefault(f => f.Name == name);

        if (faculty?.Name != null)
        {
            Response.StatusCode = StatusCodes.Status409Conflict;
            return new
            {
                Message = $"Факультет {name} уже есть в базе данных"
            };
        }

        _context.Faculties.Add(new Faculty()
        {
            Name = name
        });
        
        await _context.SaveChangesAsync();

        Response.StatusCode = StatusCodes.Status200OK;
        return new
        {
            Message = $"Факультет c именем {name} успешно добавлен в базу данных"
        };
    }

    [HttpGet, Route("Tools/GetFaculties")]
    public List<Faculty> GetFaculties()
        => _context.Faculties.ToList();

    [HttpPost, Route("Tools/AddStudyPlan")]
    public async Task<object> AddStudyPlan(string studyTypeId, string facultyId, string courseId)
    {   
        var studyPlan = _context.StudyPlans
            .Select(f => f)
            .FirstOrDefault(f => f.StudyTypeId.ToString() == studyTypeId
                                 && f.FacultyId.ToString() == facultyId 
                                 && f.CourseId.ToString() == courseId);

        if (studyPlan != null)
        {
            Response.StatusCode = StatusCodes.Status409Conflict;
            return new
            {
                Message = "Такой учебный план уже есть в базе данных"
            };
        }

        var studyType = _context.StudyTypes
            .Select(f => f)
            .FirstOrDefault(f => f.Id.ToString() == studyTypeId);

        var faculty = _context.Faculties
            .Select(f => f)
            .FirstOrDefault(f => f.Id.ToString() == facultyId);

        var course = _context.Courses
            .Select(f => f)
            .FirstOrDefault(f => f.Id.ToString() == courseId);

        if (studyType != null && faculty != null && course != null)
        {
            _context.StudyPlans.Add(new StudyPlan()
            {
                StudyTypeId = studyType.Id,
                FacultyId = faculty.Id,
                CourseId = course.Id
            });
            
            await _context.SaveChangesAsync();

            Response.StatusCode = StatusCodes.Status200OK;
            return new
            {
                Message = "Учебный план успешно добавлен в базу данных"
            };
        }
        
        Response.StatusCode = StatusCodes.Status404NotFound;
        return new
        {
            Message = "Нету таких studyType/faculty/course в базе данных"
        };
    }

    [HttpGet, Route("Tools/GetStudyPlans")]
    public List<StudyPlan> GetStudyPlans()
        => _context.StudyPlans.ToList();
    
    [HttpPost, Route("Tools/AddCourse")]
    public async Task<object> AddCourse(string studyProgramId, string name)
    {
        var faculty = _context.Courses
            .Select(f => f)
            .FirstOrDefault(f => f.Name == name);

        if (faculty?.Name != null)
        {
            Response.StatusCode = StatusCodes.Status409Conflict;
            return new
            {
                Message = $"Такой профиль подготовки {name} уже есть в базе данных"
            };
        }
        
        var studyProgram = _context.StudyPrograms
            .Select(f => f)
            .FirstOrDefault(f => f.Id.ToString() == studyProgramId);

        if (studyProgram != null)
        {
            _context.Courses.Add(new Course()
            {
                Name = name,
                StudyProgramId = studyProgram.Id
            });
            
            await _context.SaveChangesAsync();

            Response.StatusCode = StatusCodes.Status200OK;
            return new
            {
                Message = $"Профиль подготовки {name} успешно добавлен в базу данных"
            };
        }

        Response.StatusCode = StatusCodes.Status404NotFound;
        return new
        {
            Message = "Нету таких studyProgram в базе данных"
        };
    }

    [HttpGet, Route("Tools/GetCourses")]
    public List<Course> GetCourses()
        => _context.Courses.ToList();
    
    [HttpPost, Route("Tools/AddStudyType")]
    public async Task<object> AddStudyType(string name)
    {
        var studyType = _context.StudyTypes
            .Select(f => f)
            .FirstOrDefault(f => f.StudyTypeName == name);

        if (studyType != null)
        {
            Response.StatusCode = StatusCodes.Status409Conflict;
            return new
            {
                Message = $"Такая форма обучения {name} уже есть в базе данных"
            };
        }
        
        _context.StudyTypes.Add(new StudyType()
        {
            StudyTypeName = name
        });
        
        await _context.SaveChangesAsync();

        Response.StatusCode = StatusCodes.Status200OK;
        return new
        {
            Message = $"Форма обучения {name} успешно добавлен в базу данных"
        };
    }

    [HttpGet, Route("Tools/GetStudyTypes")]
    public List<StudyType> GetStudyTypes()
        => _context.StudyTypes.ToList();
    
    [HttpPost, Route("Tools/AddStudyProgram")]
    public async Task<object> AddStudyProgram(string name)
    {
        var studyProgram = _context.StudyPrograms
            .Select(f => f)
            .FirstOrDefault(f => f.Name == name);
        
        if (studyProgram?.Name != null)
        {
            Response.StatusCode = StatusCodes.Status409Conflict;
            return new
            {
                Message = $"Направление обучения {name} уже есть в базе данных"
            };
        }

        _context.StudyPrograms.Add(new StudyProgram()
        {
            Name = name
        });
        
        await _context.SaveChangesAsync();

        Response.StatusCode = StatusCodes.Status200OK;
        return new
        {
            Message = $"Направление обучения {name} успешно добавлен в базу данных"
        };
    }

    [HttpGet, Route("Tools/GetStudyPrograms")]
    public List<StudyProgram> GetStudyPrograms()
        => _context.StudyPrograms.ToList();

    [HttpPost, Route("Tools/AddDepartment")]
    public async Task<object> AddDepartment(string name, string facultyId)
    {
        var department = _context.Departments
            .Select(f => f)
            .FirstOrDefault(f => f.Name == name);
        
        if (department?.Name != null)
        {
            Response.StatusCode = StatusCodes.Status409Conflict;
            return new
            {
                Message = $"Кафедра {name} уже есть в базе данных"
            };
        }
        
        var faculty = _context.Faculties
            .Select(f => f)
            .FirstOrDefault(f => f.Id.ToString() == facultyId);

        if (faculty != null)
        {
            _context.Departments.Add(new Department()
            {
                Name = name,
                FacultyId = faculty.Id
            });
            
            await _context.SaveChangesAsync();

            Response.StatusCode = StatusCodes.Status200OK;
            return new
            {
                Message = $"Кафедра {name} успешно добавлен в базу данных"
            };
        }
        
        Response.StatusCode = StatusCodes.Status404NotFound;
        return new
        {
            Message = "Нету таких faculty в базе данных"
        };
    }

    [HttpGet, Route("Tools/GetDepartments")]
    public List<Department> GetDepartments()
        => _context.Departments.ToList();
    
    [HttpPost, Route("Tools/AddPractice")]
    public async Task<object> AddPractice(int semestr, string studyPlanId, string practiceTypeId)
    {
        var practice = _context.Practices
            .Select(f => f)
            .FirstOrDefault(f => f.Semester == semestr && f.StudyPlanId.ToString() == studyPlanId
                                                       && f.PracticeTypeId.ToString() == practiceTypeId);

        if (practice != null)
        {
            Response.StatusCode = StatusCodes.Status409Conflict;
            return new
            {
                Message = "Такая праткика уже есть в базе данных"
            };
        }
        
        var studyPlan = _context.StudyPlans
            .Select(f => f)
            .FirstOrDefault(f => f.Id.ToString() == studyPlanId);
        
        var practiceType = _context.PracticeTypes
            .Select(f => f)
            .FirstOrDefault(f => f.Id.ToString() == practiceTypeId);

        if (studyPlan != null && practiceType != null)
        {
            _context.Practices.Add(new Practice()
            {
                Semester = semestr,
                PracticeTypeId = practiceType.Id,
                StudyPlanId =studyPlan.Id
            });
            
            await _context.SaveChangesAsync();

            Response.StatusCode = StatusCodes.Status200OK;
            return new
            {
                Message = $"Практика в {semestr} семестр успешно добавлен в базу данных"
            };
        }
        
        Response.StatusCode = StatusCodes.Status404NotFound;
        return new
        {
            Message = "Нету таких practiceType/studyPlan в базе данных"
        };
    }

    [HttpGet, Route("Tools/GetPractices")]
    public List<Practice> GetPractices()
        => _context.Practices.ToList();
    
    [HttpPost, Route("Tools/AddPracticePeriod")]
    public async Task<object> AddPracticePeriod(string practiceId, DateTime start, DateTime end)
    {
        var practicePeriod = _context.PracticePeriods
            .Select(f => f)
            .FirstOrDefault(f => f.PracticeId.ToString() == practiceId &&
                                 f.PracticeStart == start && f.PracticeEnd == end);

        if (practicePeriod != null)
        {
            Response.StatusCode = StatusCodes.Status409Conflict;
            return new
            {
                Message = "Такой период практики уже есть в базе данных"
            };
        }
        
        var practice = _context.Practices
            .Select(f => f)
            .FirstOrDefault(f => f.Id.ToString() == practiceId);

        if (practice != null)
        {
            _context.PracticePeriods.Add(new PracticePeriod()
            {
                PracticeId = practice.Id,
                PracticeStart = start,
                PracticeEnd = end
            });
            
            await _context.SaveChangesAsync();

            Response.StatusCode = StatusCodes.Status200OK;
            return new
            {
                Message = $"Период практики в {start} {end} успешно добавлен в базу данных"
            };
        }
        
        Response.StatusCode = StatusCodes.Status404NotFound;
        return new
        {
            Message = "Нету таких practice в базе данных"
        };
    }

    [HttpGet, Route("Tools/GetPracticePeriods")]
    public List<PracticePeriod> GetPracticePeriods()
        => _context.PracticePeriods.ToList();
    
    [HttpPost, Route("Tools/AddPracticeKind")]
    public async Task<object> AddPracticeKind(string name)
    {
        var department = _context.PracticeKinds
            .Select(f => f)
            .FirstOrDefault(f => f.Name == name);
        
        if (department?.Name != null)
        {
            Response.StatusCode = StatusCodes.Status409Conflict;
            return new
            {
                Message = $"Вид практики {name} уже есть в базе данных"
            };
        }
        
        _context.PracticeKinds.Add(new PracticeKind()
        {
            Name = name,
        });
        
        await _context.SaveChangesAsync();

        Response.StatusCode = StatusCodes.Status200OK;
        return new
        {
            Message = $"Вид практики {name} успешно добавлен в базу данных"
        };
    }

    [HttpGet, Route("Tools/GetPracticesKind")]
    public List<PracticeKind> GetPracticesKind()
        => _context.PracticeKinds.ToList();
    
    [HttpPost, Route("Tools/AddPracticeType")]
    public async Task<object> AddPracticeType(string name, string practiceKindId)
    {
        var practiceType = _context.PracticeTypes
            .Select(f => f)
            .FirstOrDefault(f => f.Name == name);
        
        if (practiceType?.Name != null)
        {
            Response.StatusCode = StatusCodes.Status409Conflict;
            return new
            {
                Message = $"Тип практики {name} уже есть в базе данных"
            };
        }
        
        var practiceKind = _context.PracticeKinds
            .Select(f => f)
            .FirstOrDefault(f => f.Id.ToString() == practiceKindId);

        if (practiceKind != null)
        {
            _context.PracticeTypes.Add(new PracticeType()
            {
                Name = name,
                PracticeKindId = practiceKind.Id
            });
            
            await _context.SaveChangesAsync();

            Response.StatusCode = StatusCodes.Status200OK;
            return new
            {
                Message = $"Тип практики {name} успешно добавлен в базу данных"
            };
        }
        
        Response.StatusCode = StatusCodes.Status404NotFound;
        return new
        {
            Message = $"PracticeKinds отсутствует в базе данных"
        };
    }

    [HttpGet, Route("Tools/GetPracticesType")]
    public List<PracticeType> GetPracticesType()
        => _context.PracticeTypes.ToList();
            
    // [HttpPost, Route("Tools/AddPracticeCompany")]
    // public async Task<object> AddPracticeCompany(string cityName, string companyAddress, string companyEmail,
    //     int itnNumber, string companyName, string companyShortName, string signatoryName, string signatoryLastName,
    //     string signatoryJob, string signatoryPatronymic, string signatoryPhoneNumber, string signatoryEmail,
    //     bool isSignatory, bool isHead)
    // {
    //     var city = new City()
    //     {
    //         Name = cityName
    //     };
    //     
    //     _context.Add(city);
    //     await _context.SaveChangesAsync();
    //     
    //     var company = new Company()
    //     {
    //         CityId = city.Id,
    //         Address = companyAddress,
    //         Email = companyEmail,
    //         Itn = itnNumber,
    //         Name = companyName,
    //         ShortName = companyShortName
    //     };
    //         
    //     _context.Add(company);
    //     await _context.SaveChangesAsync();
    //     
    //     var signatory = new Signatory()
    //     {
    //         Name = signatoryName[0].ToString(),
    //         LastName = signatoryLastName,
    //         Patronymic = signatoryPatronymic[0].ToString(),
    //         Job = signatoryJob
    //     };
    //         
    //     _context.Add(signatory);
    //     await _context.SaveChangesAsync();
    //     
    //     var contact = new ContactPerson()
    //     {       
    //         Name = signatoryName,
    //         LastName = signatoryLastName,
    //         Patronymic = signatoryPatronymic,
    //         PhoneNumber = signatoryPhoneNumber,
    //         CompanyId = company.Id,
    //         Email = signatoryEmail,
    //         Head = isHead,
    //         Signatory = isSignatory
    //     };
    //         
    //     _context.Add(contact);
    //     await _context.SaveChangesAsync();
    //     
    //     Response.StatusCode = StatusCodes.Status200OK;
    //     return new { Message = "Новая компания успешно добавлена."};
    // }
    
    // [HttpPost, Route("Tools/AddPractice")]
    // public async Task<object> AddPractice(string practiceKindName, string practiceTypeName, int practiceSemestr,
    //     string authorityNumber, DateTime authorityStart, DateTime practiceStart, DateTime practiceEnd, int contractNumber,
    //     DateTime contractDate)
    // {
    //     var practiceKind = new PracticeKind()
    //     {
    //         Name = practiceKindName
    //     };
    //         
    //     _context.Add(practiceKind);
    //     await _context.SaveChangesAsync();
    //     
    //     var practiceType = new PracticeType()
    //     {
    //         PracticeKindId = practiceKind.Id,
    //         Name = practiceTypeName
    //     };
    //         
    //     _context.Add(practiceType);
    //     await _context.SaveChangesAsync();
    //     
    //     var studentGroup = _context.StudentGroups
    //         .Select(s => s)
    //         .First(s => s.StudentId.ToString() == id);
    //     
    //     var group = _context.Groups
    //         .Select(s => s)
    //         .First(s => s.Id.ToString() == studentGroup.GroupId.ToString());
    //     
    //     var practice = new Practice()
    //     {
    //         StudyPlanId = group.StudyPlanId,
    //         PracticeTypeId = practiceType.Id,
    //         Semester = practiceSemestr
    //     };
    //     
    //     _context.Add(practice);
    //     await _context.SaveChangesAsync();
    //     
    //     var authority = new Authority()
    //     {
    //          SignatoryId = signatory.Id,
    //          Number = authorityNumber,
    //          Start = authorityStart
    //     };
    //         
    //     _context.Add(authority);
    //     await _context.SaveChangesAsync();
    //
    //     var practicePeriod = new PracticePeriod()
    //     {
    //         PracticeId = practice.Id,
    //         PracticeStart = practiceStart,
    //         PracticeEnd = practiceEnd, 
    //     };
    //         
    //     _context.Add(practicePeriod);
    //     await _context.SaveChangesAsync();
    //     
    //     var contract = new Contract()
    //     {
    //         CompanyId = company.Id,
    //         SignatoryId = signatory.Id,
    //         ContactPersonId = contact.Id,
    //         Number = contractNumber,
    //         Date = contractDate
    //     };
    //         
    //     _context.Add(contract);
    //     await _context.SaveChangesAsync();
    //     
    //     var practiceContract = new PracticeContract()
    //     {
    //         PracticeId = practice.Id,
    //         ContractId = contract.Id,
    //     };
    //         
    //     _context.Add(practiceContract);
    //     await _context.SaveChangesAsync();
    // }
}

