using ModulyBack.Moduly.Domain.Model.Commands;
using ModulyBack.Moduly.Interfaces.REST.Resources;

namespace ModulyBack.Moduly.Interfaces.REST.Transform;

public static class AssignPermissionCommandFromResourceAssembler
{
    public static AssignPermissionCommand ToCommandFromResource(AssignPermissionResource resource)
    {
        return new AssignPermissionCommand
        {
            AssignerUserId = resource.AssignerUserId,
            TargetUserId = resource.TargetUserId,
            CompanyId = resource.CompanyId,
            ModuleId = resource.ModuleId,
            PermissionTypeId = resource.PermissionTypeId,
            AllowedAction = resource.AllowedAction
        };
    }
}