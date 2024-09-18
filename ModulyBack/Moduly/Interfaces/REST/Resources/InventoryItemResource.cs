namespace ModulyBack.Moduly.Interfaces.REST.Resources;

public class InventoryItemResource
{
    public Guid Id { get; set; }
    public Guid BeingId { get; set; }
    public int Quantity { get; set; }
    public DateTime LastUpdated { get; set; }
}