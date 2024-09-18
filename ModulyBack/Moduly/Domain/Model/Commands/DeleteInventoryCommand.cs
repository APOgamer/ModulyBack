namespace ModulyBack.Moduly.Domain.Model.Commands;

public record DeleteInventoryCommand(Guid InventoryId, Guid UserId);
