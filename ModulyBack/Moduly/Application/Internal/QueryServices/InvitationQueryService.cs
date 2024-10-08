using ModulyBack.Moduly.Domain.Model.Entities;
using ModulyBack.Moduly.Domain.Model.Queries;
using ModulyBack.Moduly.Domain.Repositories;
using ModulyBack.Moduly.Domain.Services;

namespace ModulyBack.Moduly.Application.Internal.QueryServices;

public class InvitationQueryService : IInvitationQueryService
{
    private readonly IInvitationRepository _invitationRepository;

    public InvitationQueryService(IInvitationRepository invitationRepository)
    {
        _invitationRepository = invitationRepository;
    }

    public async Task<Invitation> Handle(GetInvitationByIdQuery query)
    {
        return await _invitationRepository.FindByIdAsync(query.Id);
    }
}