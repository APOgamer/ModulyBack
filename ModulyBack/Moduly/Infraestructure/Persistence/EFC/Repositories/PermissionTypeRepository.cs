using Microsoft.EntityFrameworkCore;
using ModulyBack.Moduly.Domain.Model.Aggregate;
using ModulyBack.Moduly.Domain.Repositories;
using ModulyBack.Shared.Infraestructure.Persistences.EFC.Configuration;
using ModulyBack.Shared.Infraestructure.Persistences.EFC.Repositories;

namespace ModulyBack.Moduly.Infraestructure.Persistence.EFC.Repositories;

public class PermissionTypeRepository : BaseRepository<PermissionType>, IPermissionTypeRepository
{
    public PermissionTypeRepository(AppDbContext context) : base(context) { }

    public async Task AddAsync(PermissionType permissionType)
    {
        await Context.Set<PermissionType>().AddAsync(permissionType);
        await Context.SaveChangesAsync();
    }
    public async Task<IEnumerable<PermissionType>> GetPermissionTypesByCompanyId(Guid companyId)
    {
        return await Context.PermissionTypes
            .Where(pt => pt.CompanyId == companyId)
            .ToListAsync();
    }
    public async Task<PermissionType?> GetByIdAsync(Guid id)
    {
        return await Context.Set<PermissionType>().FindAsync(id);
    }
}