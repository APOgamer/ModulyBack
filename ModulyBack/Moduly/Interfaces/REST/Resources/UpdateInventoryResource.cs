// UpdateInventoryResource
namespace ModulyBack.Moduly.Interfaces.REST.Resources;

public record UpdateInventoryResource(
    string Name,
    string Description,
    decimal UnitPrice,
    Guid UserId
);