using Microsoft.EntityFrameworkCore;
using ModulyBack.Moduly.Domain.Model.Entities;
using ModulyBack.Moduly.Domain.Repositories;
using ModulyBack.Shared.Infraestructure.Persistences.EFC.Configuration;
using ModulyBack.Shared.Infraestructure.Persistences.EFC.Repositories;

namespace ModulyBack.Moduly.Infraestructure.Persistence.EFC.Repositories;

public class CompanyRepository : BaseRepository<Company>, ICompanyRepository
{
    public CompanyRepository(AppDbContext context) : base(context) { }

    public async Task<Company> FindByIdAsync(Guid id)
    {
        return await Context.Set<Company>().FindAsync(id);
    }

    public async Task<IEnumerable<Company>> GetAllAsync()
    {
        return await Context.Set<Company>().ToListAsync();
    }

    public async Task AddAsync(Company company)
    {
        await Context.Set<Company>().AddAsync(company);
    }

    public async Task UpdateAsync(Company company)
    {
        Context.Set<Company>().Update(company);
        await Context.SaveChangesAsync();
    }

    public async Task<bool> ExistsByIdAsync(Guid id)
    {
        return await Context.Set<Company>().AnyAsync(c => c.Id == id);
    }
    public async Task<Company?> FindByModuleIdAsync(Guid moduleId)
    {
        return await Context.Set<Company>()
            .Include(c => c.Modules)
            .FirstOrDefaultAsync(c => c.Modules.Any(m => m.Id == moduleId));
    }
    public async Task<Company?> GetCompanyByModuleIdAsync(Guid moduleId)
    {
        return await Context.Set<Company>()
            .FirstOrDefaultAsync(c => c.Modules.Any(m => m.Id == moduleId));
    }
    public async Task<IEnumerable<Company>> FindByCreatorIdAsync(Guid creatorId)
    {
        return await Context.Set<Company>()
            .Where(c => c.CreatedById == creatorId)
            .ToListAsync();
    }
}