using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PizzaHub.Domain.Entities;

[Table("TB_Accounts")]
public class Client
{
    [Key] public Guid AccountId { get; set; } = Guid.NewGuid();
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    public string Name { get; set; }
    public string Email { get; set; }
    public bool EmailConfirmed { get; set; } = false;
    public string Code { get; set; } = string.Empty!;
    public string Password { get; set; }
    public string Phone { get; set; }
}