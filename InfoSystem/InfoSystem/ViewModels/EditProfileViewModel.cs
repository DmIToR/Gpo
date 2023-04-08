using System.ComponentModel.DataAnnotations;

namespace InfoSystem.ViewModels;

public class EditProfileViewModel
{
    [Required]
    public string Name { get; set; }
    
    [Required]
    public string Surname { get; set; }
    
    [Required]
    public string Patronymic { get; set; }
}