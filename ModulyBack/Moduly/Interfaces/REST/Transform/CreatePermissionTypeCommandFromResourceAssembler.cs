using ModulyBack.Moduly.Domain.Model.Commands;
using ModulyBack.Moduly.Interfaces.REST.Resources;

namespace ModulyBack.Moduly.Interfaces.REST.Transform;

public static class CreatePermissionTypeCommandFromResourceAssembler
{
    public static CreatePermissionTypeCommand ToCommand(CreatePermissionTypeResource resource)
    {
        return new CreatePermissionTypeCommand
        {
            Name = resource.Name,
            Description = resource.Description,
            CompanyId = resource.CompanyId,
            AllowedActions = resource.AllowedActions,
            UserId = resource.UserId
        };
    }
}
