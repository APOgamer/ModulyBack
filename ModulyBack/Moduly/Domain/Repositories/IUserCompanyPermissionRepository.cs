using ModulyBack.Moduly.Domain.Model.Aggregate;
using ModulyBack.Moduly.Domain.Model.ValueObjects;
using ModulyBack.Shared.Domain.Repositories;

namespace ModulyBack.Moduly.Domain.Repositories;

public interface IUserCompanyPermissionRepository : IBaseRepository<UserCompanyPermission>
{
    Task<UserCompanyPermission?> FindByUserCompanyAndPermissionTypeAsync(Guid userCompanyId, AllowedActionEnum permissionType);

}
