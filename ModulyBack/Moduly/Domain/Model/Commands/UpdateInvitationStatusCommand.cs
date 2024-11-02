namespace ModulyBack.Moduly.Domain.Model.Commands;

public record UpdateInvitationStatusCommand(
    Guid Id,
    string Status
);