using ModulyBack.Moduly.Domain.Model.Aggregate;
using ModulyBack.Moduly.Domain.Model.Commands;

namespace ModulyBack.Moduly.Domain.Services;

public interface IPermissionTypeCommandService
{
    Task<PermissionType> Handle(CreatePermissionTypeCommand command);
    Task<IEnumerable<PermissionType>> GetPermissionTypesByCompanyId(Guid companyId);

}
