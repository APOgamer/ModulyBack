namespace ModulyBack.Moduly.Domain.Model.Commands;

public record UpdateInventoryCommand(
    Guid InventoryId,
    string Name,
    string Description,
    decimal UnitPrice,
    Guid UserId
);