using ModulyBack.Moduly.Domain.Model.Entities;
using ModulyBack.Shared.Domain.Repositories;

namespace ModulyBack.Moduly.Domain.Repositories;

public interface IInvitationRepository : IBaseRepository<Invitation>
{
    //Task<Invitation> AddAsync(Invitation invitation);
    Task<Invitation> FindByIdAsync(Guid id);
    Task<Invitation> UpdateAsync(Invitation invitation);
    Task RemoveAsync(Invitation invitation);
    Task<IEnumerable<Invitation>> FindPendingByUserIdAsync(Guid userId);
    Task<IEnumerable<Invitation>> FindSentByTransmitterIdAsync(Guid transmitterId);
}