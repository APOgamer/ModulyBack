using ModulyBack.Moduly.Domain.Model.Entities;
using ModulyBack.Shared.Domain.Repositories;

namespace ModulyBack.Moduly.Domain.Repositories;

public interface IModuleRepository : IBaseRepository<Module>
{
    Task<Module> FindByIdAsync(Guid id);
    Task<IEnumerable<Module>> GetAllAsync();
    Task<IEnumerable<Module>> GetByCompanyIdAsync(Guid companyId);
    Task AddAsync(Module module);
    Task UpdateAsync(Module module);
    Task RemoveAsync(Module module);
}