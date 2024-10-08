using ModulyBack.Moduly.Domain.Model.Aggregate;

namespace ModulyBack.Moduly.Domain.Repositories;

public interface IUserCompanyPermissionsRepository
{
    Task<List<UserCompanyPermission>> FindByUserCompanyIdAsync(Guid userCompanyId);
}