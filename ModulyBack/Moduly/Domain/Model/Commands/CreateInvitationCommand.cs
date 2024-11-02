namespace ModulyBack.Moduly.Domain.Model.Commands;

public record CreateInvitationCommand(
    Guid UserId,
    Guid TransmitterId,
    Guid CompanyId
);