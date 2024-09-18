// CreateInventoryResource
namespace ModulyBack.Moduly.Interfaces.REST.Resources;

public record CreateInventoryResource(
    string Name,
    string Description,
    int InitialStock,
    decimal UnitPrice,
    Guid ModuleId,
    Guid UserId
);