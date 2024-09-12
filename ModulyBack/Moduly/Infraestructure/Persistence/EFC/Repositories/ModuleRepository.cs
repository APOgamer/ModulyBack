using Microsoft.EntityFrameworkCore;
using ModulyBack.Moduly.Domain.Model.Entities;
using ModulyBack.Moduly.Domain.Repositories;
using ModulyBack.Shared.Infraestructure.Persistences.EFC.Configuration;
using ModulyBack.Shared.Infraestructure.Persistences.EFC.Repositories;

namespace ModulyBack.Moduly.Infraestructure.Persistence.EFC.Repositories;

public class ModuleRepository : BaseRepository<Module>, IModuleRepository
{
    public ModuleRepository(AppDbContext context) : base(context) { }

    public async Task<Module> FindByIdAsync(Guid id)
    {
        return await Context.Set<Module>().FindAsync(id);
    }

    public async Task<IEnumerable<Module>> GetAllAsync()
    {
        return await Context.Set<Module>().ToListAsync();
    }

    public async Task<IEnumerable<Module>> GetByCompanyIdAsync(Guid companyId)
    {
        return await Context.Set<Module>()
            .Where(m => m.CompanyId == companyId)
            .ToListAsync();
    }

    public async Task AddAsync(Module module)
    {
        await Context.Set<Module>().AddAsync(module);
    }

    public async Task UpdateAsync(Module module)
    {
        Context.Set<Module>().Update(module);
        await Context.SaveChangesAsync();
    }

    public async Task RemoveAsync(Module module)
    {
        Context.Set<Module>().Remove(module);
        await Context.SaveChangesAsync();
    }
}