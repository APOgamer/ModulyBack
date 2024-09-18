namespace ModulyBack.Moduly.Domain.Model.Commands;

public record RemoveStockCommand(Guid InventoryId, int Amount, Guid UserId);
