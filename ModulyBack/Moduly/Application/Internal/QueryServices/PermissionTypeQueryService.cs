using ModulyBack.Moduly.Domain.Model.Aggregate;
using ModulyBack.Moduly.Domain.Repositories;
using ModulyBack.Moduly.Domain.Services;

namespace ModulyBack.Moduly.Application.Internal.QueryServices;

public class PermissionTypeQueryService : IPermissionTypeQueryService
{
    private readonly IPermissionTypeRepository _permissionTypeRepository;

    public PermissionTypeQueryService(IPermissionTypeRepository permissionTypeRepository)
    {
        _permissionTypeRepository = permissionTypeRepository;
    }

    public async Task<PermissionType?> GetPermissionTypeByIdAsync(Guid permissionTypeId)
    {
        return await _permissionTypeRepository.FindByIdAsync(permissionTypeId);
    }
}