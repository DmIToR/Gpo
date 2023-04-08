using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using InfoSystem.Entities;
using Microsoft.EntityFrameworkCore;

namespace InfoSystem.Models;

public abstract class Profile
{
    [Key, ForeignKey(nameof(User))]
    public Guid Id { get; set; }
    public string? Name { set; get; }
    public string? Surname { set; get; }
    public string? Patronymic { set; get; }
}