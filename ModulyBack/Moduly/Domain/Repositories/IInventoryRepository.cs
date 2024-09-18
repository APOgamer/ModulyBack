using ModulyBack.Moduly.Domain.Model.Entities;
using ModulyBack.Shared.Domain.Repositories;

namespace ModulyBack.Moduly.Domain.Repositories
{
    public interface IInventoryRepository : IBaseRepository<Inventory>
    {
        Task<Inventory?> FindByIdAsync(Guid id);
        Task<IEnumerable<Inventory>> ListAsync();
        Task<IEnumerable<Inventory>> FindByModuleIdAsync(Guid moduleId);
    }
}