namespace ModulyBack.Moduly.Interfaces.REST.Resources;

public class BeingResource
{
    public Guid Id { get; set; }
    public string IdentifierName { get; set; } = string.Empty;
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public string? Category { get; set; }
    public DateTime? ManufactureDate { get; set; }
    public DateTime? ExpiryDate { get; set; }
    public string? Origin { get; set; }
    public ICollection<string> Tags { get; set; } = new List<string>();
    public bool IsActive { get; set; }
    public decimal? Discount { get; set; }
    public string Currency { get; set; } = "USD";
    public string? ImageUrl { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public Guid ModuleId { get; set; }
}