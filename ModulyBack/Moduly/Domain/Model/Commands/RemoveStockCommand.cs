namespace ModulyBack.Moduly.Domain.Model.Commands;

public record RemoveStockCommand(Guid InventoryId, Guid BeingId, int Quantity, Guid UserId);
