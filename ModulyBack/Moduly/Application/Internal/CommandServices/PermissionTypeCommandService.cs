using ModulyBack.Moduly.Domain.Model.Aggregate;
using ModulyBack.Moduly.Domain.Model.Commands;
using ModulyBack.Moduly.Domain.Repositories;
using ModulyBack.Moduly.Domain.Services;

namespace ModulyBack.Moduly.Application.Internal.CommandServices;

public class PermissionTypeCommandService : IPermissionTypeCommandService
{
    private readonly IPermissionTypeRepository _permissionTypeRepository;

    public PermissionTypeCommandService(IPermissionTypeRepository permissionTypeRepository)
    {
        _permissionTypeRepository = permissionTypeRepository;
    }

    public async Task<PermissionType> Handle(CreatePermissionTypeCommand command)
    {
        var permissionType = new PermissionType
        {
            Id = Guid.NewGuid(),
            Name = command.Name,
            Description = command.Description,
            CompanyId = command.CompanyId,
            PermissionTypeActions = command.AllowedActions
                .Select(action => new PermissionTypeAction
                {
                    Id = Guid.NewGuid(),
                    AllowedAction = action
                }).ToList()
        };

        await _permissionTypeRepository.AddAsync(permissionType);
        return permissionType;
    }
    public async Task<IEnumerable<PermissionType>> GetPermissionTypesByCompanyId(Guid companyId)
    {
        return await _permissionTypeRepository.GetPermissionTypesByCompanyId(companyId);
    }
}
