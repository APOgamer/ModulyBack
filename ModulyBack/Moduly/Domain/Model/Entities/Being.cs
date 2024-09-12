using System.ComponentModel.DataAnnotations;
using ModulyBack.Moduly.Domain.Model.Aggregate;

namespace ModulyBack.Moduly.Domain.Model.Entities;

public class Being
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public string IdentifierName { get; set; } = string.Empty;

    public string? Description { get; set; }

    [Range(0, double.MaxValue)]
    public decimal Price { get; set; }

    public string? Category { get; set; }

    public DateTime? ManufactureDate { get; set; }

    public DateTime? ExpiryDate { get; set; }

    public string? Origin { get; set; }

    public ICollection<string> Tags { get; set; } = new List<string>();

    public bool IsActive { get; set; } = true;

    public decimal? Discount { get; set; }

    public string? Currency { get; set; } = "USD";

    public string? ImageUrl { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }

    [Required]
    public ICollection<BeingModule> BeingModules { get; set; } = new List<BeingModule>();
}
