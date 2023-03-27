using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InfoSystem.Models;

public class head_enterprise
{
    [Key, ForeignKey(nameof(User))] public Guid head_enterpriseId { get; set; }
    public string Phone { get; set; }
    public string Org_id { get; set; } // Пока-что заглушка

}
