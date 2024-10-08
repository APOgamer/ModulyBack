using ModulyBack.Moduly.Domain.Model.Entities;
using ModulyBack.Moduly.Interfaces.REST.Resources;

namespace ModulyBack.Moduly.Interfaces.REST.Transform;

public static class InvitationResourceFromEntityAssembler
{
    public static InvitationResource ToResourceFromEntity(Invitation invitation)
    {
        return new InvitationResource
        {
            Id = invitation.Id,
            UserId = invitation.UserId,
            TransmitterId = invitation.TransmitterId,
            CompanyId = invitation.CompanyId,
            Status = invitation.Status
        };
    }
}