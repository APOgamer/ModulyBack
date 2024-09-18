using ModulyBack.Moduly.Interfaces.REST.Resources;

public class InventoryResource
{
    public Guid Id { get; set; }
    public Guid ModuleId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public ICollection<InventoryItemResource> Items { get; set; }
    public int TotalQuantity { get; set; }
    public bool IsInStock { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}