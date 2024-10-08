using Microsoft.EntityFrameworkCore;
using ModulyBack.Moduly.Domain.Model.Aggregate;
using ModulyBack.Moduly.Domain.Repositories;
using ModulyBack.Shared.Infraestructure.Persistences.EFC.Configuration;
using ModulyBack.Shared.Infraestructure.Persistences.EFC.Repositories;

namespace ModulyBack.Moduly.Infraestructure.Persistence.EFC.Repositories;

public class UserCompanyPermissionsRepository : BaseRepository<UserCompanyPermission>, IUserCompanyPermissionsRepository
{
    public UserCompanyPermissionsRepository(AppDbContext context) : base(context) { }

    public async Task<List<UserCompanyPermission>> FindByUserCompanyIdAsync(Guid userCompanyId)
    {
        return await Context.UserCompanyPermissions
            .Where(ucp => ucp.UserCompanyId == userCompanyId)
            .ToListAsync();
    }
}