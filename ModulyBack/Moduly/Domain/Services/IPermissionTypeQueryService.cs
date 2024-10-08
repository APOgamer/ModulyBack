using ModulyBack.Moduly.Domain.Model.Aggregate;

namespace ModulyBack.Moduly.Domain.Services;

public interface IPermissionTypeQueryService
{
    Task<PermissionType?> GetPermissionTypeByIdAsync(Guid permissionTypeId);
}