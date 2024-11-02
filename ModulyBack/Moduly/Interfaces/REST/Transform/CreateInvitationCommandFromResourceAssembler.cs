using ModulyBack.Moduly.Domain.Model.Commands;
using ModulyBack.Moduly.Interfaces.REST.Resources;

namespace ModulyBack.Moduly.Interfaces.REST.Transform;

public static class CreateInvitationCommandFromResourceAssembler
{
    public static CreateInvitationCommand ToCommandFromResource(CreateInvitationResource resource)
    {
        return new CreateInvitationCommand(
            resource.UserId,
            resource.TransmitterId,
            resource.CompanyId
        );
    }
}