using Microsoft.EntityFrameworkCore;
using ModulyBack.Moduly.Domain.Model.Entities;
using ModulyBack.Moduly.Domain.Repositories;
using ModulyBack.Shared.Infraestructure.Persistences.EFC.Configuration;
using ModulyBack.Shared.Infraestructure.Persistences.EFC.Repositories;

namespace ModulyBack.Moduly.Infraestructure.Persistence.EFC.Repositories;


public class InvitationRepository : BaseRepository<Invitation>, IInvitationRepository
{
    private readonly AppDbContext _context;

    public InvitationRepository(AppDbContext context) : base(context) { }

    public async Task<Invitation> AddAsync(Invitation invitation)
    {
        await _context.Invitations.AddAsync(invitation);
        await _context.SaveChangesAsync();
        return invitation;
    }

    public async Task<Invitation> FindByIdAsync(Guid id)
    {
        return await _context.Invitations.FindAsync(id);
    }

    public async Task<Invitation> UpdateAsync(Invitation invitation)
    {
        _context.Invitations.Update(invitation);
        await _context.SaveChangesAsync();
        return invitation;
    }

    public async Task RemoveAsync(Invitation invitation)
    {
        _context.Invitations.Remove(invitation);
        await _context.SaveChangesAsync();
    }
    public async Task<IEnumerable<Invitation>> FindPendingByUserIdAsync(Guid userId)
    {
        if (_context.Invitations == null)
        {
            throw new InvalidOperationException("Invitations DbSet is null");
        }

        return await _context.Invitations
            .Where(i => i.UserId == userId && i.Status == "Sent")
            .ToListAsync();
    }

    public async Task<IEnumerable<Invitation>> FindSentByTransmitterIdAsync(Guid transmitterId)
    {
        if (_context.Invitations == null)
        {
            throw new InvalidOperationException("Invitations DbSet is null");
        }

        return await _context.Invitations
            .Where(i => i.TransmitterId == transmitterId)
            .ToListAsync();
    }
}