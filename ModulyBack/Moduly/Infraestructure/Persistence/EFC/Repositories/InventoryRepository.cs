using Microsoft.EntityFrameworkCore;
using ModulyBack.Moduly.Domain.Model.Entities;
using ModulyBack.Moduly.Domain.Repositories;
using ModulyBack.Shared.Infraestructure.Persistences.EFC.Configuration;
using ModulyBack.Shared.Infraestructure.Persistences.EFC.Repositories;

namespace ModulyBack.Moduly.Infraestructure.Persistence.EFC.Repositories
{
    public class InventoryRepository : BaseRepository<Inventory>, IInventoryRepository
    {
        public InventoryRepository(AppDbContext context) : base(context)
        {
        }

        public new async Task<Inventory?> FindByIdAsync(Guid id) =>
            await Context.Set<Inventory>().FirstOrDefaultAsync(i => i.Id == id);

        public async Task<IEnumerable<Inventory>> ListAsync() =>
            await Context.Set<Inventory>().ToListAsync();

        public async Task<IEnumerable<Inventory>> FindByModuleIdAsync(Guid moduleId) =>
            await Context.Set<Inventory>().Where(i => i.ModuleId == moduleId).ToListAsync();
    }
}