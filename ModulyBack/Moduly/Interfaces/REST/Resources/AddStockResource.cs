namespace ModulyBack.Moduly.Interfaces.REST.Resources;

public class AddStockResource
{
    public Guid UserId { get; set; }
    public Guid BeingId { get; set; }
    public int Quantity { get; set; }
}