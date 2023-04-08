using System.ComponentModel.DataAnnotations;
using InfoSystem.Models;

namespace InfoSystem.ViewModels;

public class SignUpViewModel
{
    [Required]
    public string UserName { get; set; }
    
    [Required]
    public string Email { get; set; }
    
    [Required]
    public string Password { get; set; }
    
    [Required]
    public UserRole Role { get; set; }
}