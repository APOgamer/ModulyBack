using ModulyBack.Moduly.Domain.Model.Entities;
using ModulyBack.Moduly.Interfaces.REST.Resources;
using System.Linq;

namespace ModulyBack.Moduly.Interfaces.REST.Transform
{
    public static class InventoryResourceFromEntityAssembler
    {
        public static InventoryResource ToResourceFromEntity(Inventory inventory)
        {
            return new InventoryResource
            {
                Id = inventory.Id,
                ModuleId = inventory.ModuleId,
                Name = inventory.Name,
                Description = inventory.Description,
                Items = inventory.Items.Select(item => new InventoryItemResource
                {
                    Id = item.Id,
                    BeingId = item.BeingId,
                    Quantity = item.Quantity,
                    LastUpdated = item.LastUpdated
                }).ToList(),
                TotalQuantity = inventory.TotalQuantity,
                IsInStock = inventory.IsInStock,
                CreatedAt = inventory.CreatedAt,
                UpdatedAt = inventory.UpdatedAt
            };
        }
    }
}