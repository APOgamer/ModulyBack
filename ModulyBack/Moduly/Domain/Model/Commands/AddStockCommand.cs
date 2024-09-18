namespace ModulyBack.Moduly.Domain.Model.Commands;

public record AddStockCommand(Guid InventoryId, Guid BeingId, int Quantity, Guid UserId);
