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
        await Context.PermissionTypes.AddAsync(permissionType);
    }
}