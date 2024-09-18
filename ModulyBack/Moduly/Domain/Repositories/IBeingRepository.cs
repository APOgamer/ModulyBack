using ModulyBack.Moduly.Domain.Model.Entities;
using ModulyBack.Shared.Domain.Repositories;

namespace ModulyBack.Moduly.Domain.Repositories;

public interface IBeingRepository : IBaseRepository<Being>
{
    Task<Being?> FindByIdAsync(Guid id);
    Task<IEnumerable<Being>> ListAsync();
    Task<IEnumerable<Being>> FindByModuleIdAsync(Guid moduleId);
}