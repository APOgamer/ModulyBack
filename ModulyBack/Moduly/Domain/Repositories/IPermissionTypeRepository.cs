using ModulyBack.Moduly.Domain.Model.Aggregate;
using ModulyBack.Moduly.Domain.Model.Entities;
using ModulyBack.Shared.Domain.Repositories;

namespace ModulyBack.Moduly.Domain.Repositories;

public interface IPermissionTypeRepository : IBaseRepository<PermissionType>
{
    Task AddAsync(PermissionType permissionType);
    Task<IEnumerable<PermissionType>> GetPermissionTypesByCompanyId(Guid companyId);
    Task<PermissionType?> GetByIdAsync(Guid id); // Agrega esta línea

}