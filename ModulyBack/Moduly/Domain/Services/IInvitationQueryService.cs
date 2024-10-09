using ModulyBack.Moduly.Domain.Model.Entities;
using ModulyBack.Moduly.Domain.Model.Queries;

namespace ModulyBack.Moduly.Domain.Services;

public interface IInvitationQueryService
{
    Task<Invitation> Handle(GetInvitationByIdQuery query);
    Task<IEnumerable<Invitation>> Handle(GetPendingInvitationsQuery query);
    Task<IEnumerable<Invitation>> Handle(GetSentInvitationsQuery query);
}