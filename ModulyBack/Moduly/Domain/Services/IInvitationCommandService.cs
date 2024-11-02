using ModulyBack.Moduly.Domain.Model.Commands;
using ModulyBack.Moduly.Domain.Model.Entities;

namespace ModulyBack.Moduly.Domain.Services;

public interface IInvitationCommandService
{
    Task<Invitation> Handle(CreateInvitationCommand command);
    Task<Invitation> Handle(UpdateInvitationStatusCommand command);
}