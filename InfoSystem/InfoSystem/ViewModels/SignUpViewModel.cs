using System.ComponentModel.DataAnnotations;

namespace InfoSystem.ViewModels;

public class SignUpViewModel
{
    [Required]
    public string UserName { get; set; }
    
    [Required]
    public string Email { get; set; }
    
    [Required]
    public string Password { get; set; }
}