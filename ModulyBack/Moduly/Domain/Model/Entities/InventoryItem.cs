using System.ComponentModel.DataAnnotations;

namespace ModulyBack.Moduly.Domain.Model.Entities;

public class InventoryItem
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public Guid BeingId { get; set; }
    
    public Being Being { get; set; }


    [Required]
    public int Quantity { get; set; }
    [Required]
    public Guid InventoryId { get; set; } // Foreign key reference
    public Inventory Inventory { get; set; }

    public DateTime LastUpdated { get; set; } = DateTime.UtcNow;
}