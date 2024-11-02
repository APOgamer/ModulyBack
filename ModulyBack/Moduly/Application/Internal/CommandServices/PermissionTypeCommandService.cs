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
        // Inicializar el PermissionType sin las PermissionTypeActions todavía
        var permissionType = new PermissionType
        {
            Id = Guid.NewGuid(),
            Name = command.Name,
            Description = command.Description,
            CompanyId = command.CompanyId,
            PermissionTypeActions = new List<PermissionTypeAction>() // Inicializar como lista vacía
        };

        // Ahora puedes asignar los PermissionTypeActions con el PermissionTypeId correcto
        permissionType.PermissionTypeActions = command.AllowedActions
            .Select(action => new PermissionTypeAction
            {
                Id = Guid.NewGuid(),
                PermissionTypeId = permissionType.Id, // Ahora PermissionTypeId ya existe
                AllowedAction = action
            }).ToList();

        // Guardar el PermissionType en la base de datos
        await _permissionTypeRepository.AddAsync(permissionType);

        return permissionType;
    }


    public async Task<IEnumerable<PermissionType>> GetPermissionTypesByCompanyId(Guid companyId)
    {
        return await _permissionTypeRepository.GetPermissionTypesByCompanyId(companyId);
    }
}
