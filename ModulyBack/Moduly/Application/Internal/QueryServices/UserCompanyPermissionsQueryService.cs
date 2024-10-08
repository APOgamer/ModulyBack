using ModulyBack.Moduly.Domain.Model.Aggregate;
using ModulyBack.Moduly.Domain.Repositories;
using ModulyBack.Moduly.Domain.Services;

namespace ModulyBack.Moduly.Application.Internal.QueryServices;

public class UserCompanyPermissionsQueryService : IUserCompanyPermissionsQueryService
{
    private readonly IUserCompanyPermissionsRepository _userCompanyPermissionsRepository;

    public UserCompanyPermissionsQueryService(IUserCompanyPermissionsRepository userCompanyPermissionsRepository)
    {
        _userCompanyPermissionsRepository = userCompanyPermissionsRepository;
    }

    public async Task<List<UserCompanyPermission>> GetPermissionsByUserCompanyIdAsync(Guid userCompanyId)
    {
        return await _userCompanyPermissionsRepository.FindByUserCompanyIdAsync(userCompanyId);
    }
}