using Microsoft.EntityFrameworkCore;
using ModulyBack.Moduly.Domain.Model.Aggregate;
using ModulyBack.Moduly.Domain.Model.ValueObjects;
using ModulyBack.Moduly.Domain.Repositories;
using ModulyBack.Shared.Infraestructure.Persistences.EFC.Configuration;
using ModulyBack.Shared.Infraestructure.Persistences.EFC.Repositories;

namespace ModulyBack.Moduly.Infraestructure.Persistence.EFC.Repositories;

public class UserCompanyPermissionRepository : BaseRepository<UserCompanyPermission>, IUserCompanyPermissionRepository
{
    public UserCompanyPermissionRepository(AppDbContext context) : base(context) { }

    public async Task AddAsync(UserCompanyPermission userCompanyPermission)
    {
        await Context.UserCompanyPermissions.AddAsync(userCompanyPermission);
    }
    public async Task<UserCompanyPermission?> FindByUserCompanyAndPermissionTypeAsync(Guid userCompanyId, AllowedActionEnum permissionType)
    {
        return await Context.UserCompanyPermissions
            .Include(ucp => ucp.PermissionType)
            .FirstOrDefaultAsync(ucp => ucp.UserCompanyId == userCompanyId && ucp.PermissionType.AllowedActions.Contains(permissionType));
    }

}