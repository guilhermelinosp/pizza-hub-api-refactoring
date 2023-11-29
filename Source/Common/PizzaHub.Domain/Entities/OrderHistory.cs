using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PizzaHub.Domain.Entities;

[Table("TB_OrderHistory")]
public class OrderHistory
{
    [Key] public Guid OrderHistoryd { get; set; } = Guid.NewGuid();
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    public Guid? ClientId { get; set; }
    public int Table { get; set; }
    public ICollection<Item> Items { get; set; } = new List<Item>();
    public DateTime? IsFinished { get; set; } = null;
    public DateTime? IsCanceled { get; set; } = null;
    public DateTime? IsPayment { get; set; } = null;
} 