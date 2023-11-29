using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PizzaHub.Domain.Entities;

[Table("TB_Items")]
public class Item
{
    [Key] public Guid AccountId { get; set; } = Guid.NewGuid();
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    public int Quantity { get; set; }
    public Guid ProductId { get; set; }
    public Guid OrderId { get; set; }
}