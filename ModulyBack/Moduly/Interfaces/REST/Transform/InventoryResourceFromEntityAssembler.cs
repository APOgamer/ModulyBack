// InventoryResourceFromEntityAssembler
using ModulyBack.Moduly.Domain.Model.Entities;
using ModulyBack.Moduly.Interfaces.REST.Resources;

namespace ModulyBack.Moduly.Interfaces.REST.Transform;

public static class InventoryResourceFromEntityAssembler
{
    public static InventoryResource ToResourceFromEntity(Inventory entity)
    {
        return new InventoryResource(
            entity.Id,
            entity.Name,
            entity.Description,
            entity.CurrentStock,
            entity.UnitPrice,
            entity.ModuleId
        );
    }
}