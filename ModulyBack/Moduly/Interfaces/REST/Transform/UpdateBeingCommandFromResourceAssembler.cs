using ModulyBack.Moduly.Domain.Model.Commands;
using ModulyBack.Moduly.Interfaces.REST.Resources;

namespace ModulyBack.Moduly.Interfaces.REST.Transform;

public static class UpdateBeingCommandFromResourceAssembler
{
    public static UpdateBeingCommand ToCommandFromResource(Guid id, UpdateBeingResource resource, Guid moduleId, Guid userId)
    {
        return new UpdateBeingCommand(
            id,
            resource.IdentifierName,
            resource.Description,
            resource.Price,
            resource.Category,
            resource.ManufactureDate,
            resource.ExpiryDate,
            resource.Origin,
            resource.Tags,
            resource.IsActive,
            resource.Discount,
            resource.Currency,
            resource.ImageUrl,
            moduleId,
            userId
        );
    }
}