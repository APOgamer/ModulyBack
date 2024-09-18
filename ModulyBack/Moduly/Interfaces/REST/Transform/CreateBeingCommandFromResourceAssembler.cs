using ModulyBack.Moduly.Domain.Model.Commands;
using ModulyBack.Moduly.Interfaces.REST.Resources;

namespace ModulyBack.Moduly.Interfaces.REST.Transform;

public static class CreateBeingCommandFromResourceAssembler
{
    public static CreateBeingCommand ToCommandFromResource(CreateBeingResource resource)
    {
        return new CreateBeingCommand(
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
            resource.ModuleId,
            resource.UserId
        );
    }
}