using ModulyBack.Moduly.Domain.Model.Entities;
using ModulyBack.Moduly.Interfaces.REST.Resources;

namespace ModulyBack.Moduly.Interfaces.REST.Transform
{
    public static class BeingResourceFromEntityAssembler
    {
        public static BeingResource ToResourceFromEntity(Being entity)
        {
            return new BeingResource(
                entity.Id,
                entity.IdentifierName,
                entity.Description,
                entity.Price
            )
            {
                Category = entity.Category,
                ManufactureDate = entity.ManufactureDate,
                ExpiryDate = entity.ExpiryDate,
                Origin = entity.Origin,
                Tags = entity.Tags,
                IsActive = entity.IsActive,
                Discount = entity.Discount,
                Currency = entity.Currency,
                ImageUrl = entity.ImageUrl,
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt,
                ModuleId = entity.BeingModules.FirstOrDefault()?.ModuleId ?? Guid.Empty
            };
        }
    }
}