using System.ComponentModel.DataAnnotations;

namespace InfoSystem.ViewModels;

public class DeleteUserViewModel
{
    [Required]
    public string UserName { get; set; }
}