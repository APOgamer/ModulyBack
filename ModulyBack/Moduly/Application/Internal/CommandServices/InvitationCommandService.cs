using ModulyBack.Moduly.Domain.Model.Commands;
using ModulyBack.Moduly.Domain.Model.Entities;
using ModulyBack.Moduly.Domain.Repositories;
using ModulyBack.Moduly.Domain.Services;

namespace ModulyBack.Moduly.Application.Internal.CommandServices;

public class InvitationCommandService : IInvitationCommandService
{
    private readonly IInvitationRepository _invitationRepository;
    private readonly IUserCompanyRepository _userCompanyRepository;

    public InvitationCommandService(IInvitationRepository invitationRepository,
        IUserCompanyRepository userCompanyRepository)
    {
        _invitationRepository = invitationRepository;
        _userCompanyRepository = userCompanyRepository;
    }

    public async Task<Invitation> Handle(CreateInvitationCommand command)
    {
        var invitation = new Invitation
        {
            UserId = command.UserId,
            TransmitterId = command.TransmitterId,
            CompanyId = command.CompanyId,
            Status = "Sent"
        };

        return await _invitationRepository.AddAsync(invitation);
    }

    public async Task<Invitation> Handle(UpdateInvitationStatusCommand command)
    {
        var invitation = await _invitationRepository.FindByIdAsync(command.Id);
        if (invitation == null)
        {
            throw new InvalidOperationException($"Invitation with ID {command.Id} not found.");
        }

        invitation.UpdateStatus(command.Status);

        if (command.Status == "Accepted")
        {
            var userCompany = new UserCompany
            {
                UserId = invitation.UserId,
                CompanyId = invitation.CompanyId,
                Role = "Employee", // You might want to define this role somewhere else
                JoinDate = DateTime.UtcNow
            };
            await _userCompanyRepository.AddAsync(userCompany);
        }
        else if (command.Status == "Rejected")
        {
            await _invitationRepository.RemoveAsync(invitation);
            return invitation; // Return the invitation before it's removed from the database
        }

        return await _invitationRepository.UpdateAsync(invitation);
    }
}