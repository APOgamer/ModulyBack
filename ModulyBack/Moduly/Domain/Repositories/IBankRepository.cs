using ModulyBack.Moduly.Domain.Model.Entities;
using ModulyBack.Shared.Domain.Repositories;

namespace ModulyBack.Moduly.Domain.Repositories
{
    public interface IBankRepository : IBaseRepository<Bank>
    {
        Task<IEnumerable<Bank>> GetBanksByCompanyIdAsync(Guid companyId);
        Task AddAsync(Bank bank);
        Task UpdateAsync(Bank bank);
        Task DeleteAsync(Guid id);
    }
}