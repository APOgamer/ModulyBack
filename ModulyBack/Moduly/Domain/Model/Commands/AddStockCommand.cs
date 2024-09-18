namespace ModulyBack.Moduly.Domain.Model.Commands;

public record AddStockCommand(Guid InventoryId, int Amount, Guid UserCompanyId);
