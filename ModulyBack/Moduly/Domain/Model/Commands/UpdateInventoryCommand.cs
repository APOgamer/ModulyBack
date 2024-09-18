namespace ModulyBack.Moduly.Domain.Model.Commands
{
    public record UpdateInventoryCommand(
        Guid InventoryId,
        string Name,
        string Description,
        decimal UnitPrice,
        int MinimumStockLevel,
        int MaximumStockLevel,
        int ReorderPoint,
        Guid UserId
    );
}