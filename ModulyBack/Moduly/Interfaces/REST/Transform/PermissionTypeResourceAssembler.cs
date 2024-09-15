using ModulyBack.Moduly.Domain.Model.Aggregate;
using ModulyBack.Moduly.Interfaces.REST.Resources;

namespace ModulyBack.Moduly.Interfaces.REST.Transform;

public static class PermissionTypeResourceAssembler
{
    public static PermissionTypeResource ToResource(PermissionType permissionType)
    {
        return new PermissionTypeResource
        {
            Id = permissionType.Id,
            Name = permissionType.Name,
            Description = permissionType.Description,
            CompanyId = permissionType.CompanyId,
            CompanyName = permissionType.Company.CompanyName,
            PermissionTypeActions = permissionType.PermissionTypeActions.Select(ToResource).ToList()
        };
    }

    public static PermissionTypeActionResource ToResource(PermissionTypeAction permissionTypeAction)
    {
        return new PermissionTypeActionResource
        {
            Id = permissionTypeAction.Id,
            AllowedAction = permissionTypeAction.AllowedAction.ToString()
        };
    }

    public static IEnumerable<PermissionTypeResource> ToResourceList(IEnumerable<PermissionType> permissionTypes)
    {
        return permissionTypes.Select(ToResource);
    }
}