using ModulyBack.Moduly.Domain.Model.Entities;
using ModulyBack.Shared.Domain.Repositories;

namespace ModulyBack.Moduly.Domain.Repositories;

public interface ICompanyRepository : IBaseRepository<Company>
{
    Task<Company> FindByIdAsync(Guid id);
    Task<IEnumerable<Company>> GetAllAsync();
    Task AddAsync(Company company);
    Task UpdateAsync(Company company);
    Task<bool> ExistsByIdAsync(Guid id);
    Task<Company?> FindByModuleIdAsync(Guid moduleId);
    Task<Company?> GetCompanyByModuleIdAsync(Guid moduleId);  // Nuevo método

}