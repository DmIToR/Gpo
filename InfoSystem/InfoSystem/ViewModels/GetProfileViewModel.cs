using System.ComponentModel.DataAnnotations;

namespace InfoSystem.ViewModels;

public class GetProfileViewModel
{
    [Required]
    public string UserName { get; set; }
}