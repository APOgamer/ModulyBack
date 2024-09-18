namespace ModulyBack.Moduly.Interfaces.REST.Resources;

public record InventoryResource(
    Guid Id,
    string Name,
    string Description,
    int CurrentStock,
    decimal UnitPrice,
    Guid ModuleId
);