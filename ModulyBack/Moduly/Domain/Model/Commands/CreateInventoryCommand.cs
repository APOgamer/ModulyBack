namespace ModulyBack.Moduly.Domain.Model.Commands
{
    public record CreateInventoryCommand(
        string Name,
        string Description,
        int InitialStock,
        decimal UnitPrice,
        Guid ModuleId,
        Guid UserId,
        int MinimumStockLevel,
        int MaximumStockLevel,
        int ReorderPoint
    );
}