using Microsoft.EntityFrameworkCore;
using ModulyBack.Moduly.Domain.Model.Entities;
using ModulyBack.Moduly.Domain.Repositories;
using ModulyBack.Shared.Infraestructure.Persistences.EFC.Configuration;
using ModulyBack.Shared.Infraestructure.Persistences.EFC.Repositories;

namespace ModulyBack.Moduly.Infraestructure.Persistence.EFC.Repositories;

public class BeingRepository : BaseRepository<Being>, IBeingRepository
{
    public BeingRepository(AppDbContext context) : base(context) { }

    public new async Task<Being?> FindByIdAsync(Guid id) =>
        await Context.Set<Being>()
            .Include(b => b.BeingModules)
            .FirstOrDefaultAsync(b => b.Id == id);

    public async Task<IEnumerable<Being>> ListAsync() =>
        await Context.Set<Being>()
            .Include(b => b.BeingModules)
            .ToListAsync();

    public async Task<IEnumerable<Being>> FindByModuleIdAsync(Guid moduleId) =>
        await Context.Set<Being>()
            .Include(b => b.BeingModules)
            .Where(b => b.BeingModules.Any(bm => bm.ModuleId == moduleId))
            .ToListAsync();
}