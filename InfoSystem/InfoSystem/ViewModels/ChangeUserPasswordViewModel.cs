using System.ComponentModel.DataAnnotations;

namespace InfoSystem.ViewModels;

public class ChangeUserPasswordViewModel
{
    [Required]
    public string UserName { get; set; }
    
    [Required]
    public string Password { get; set; }
}