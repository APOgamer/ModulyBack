using ModulyBack.Moduly.Domain.Model.Aggregate;

namespace ModulyBack.Moduly.Domain.Services;

public interface IUserCompanyPermissionsQueryService
{
    Task<List<UserCompanyPermission>> GetPermissionsByUserCompanyIdAsync(Guid userCompanyId);
}