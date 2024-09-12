using Microsoft.EntityFrameworkCore;
using ModulyBack.Moduly.Domain.Model.Entities;
using ModulyBack.Moduly.Domain.Repositories;
using ModulyBack.Shared.Infraestructure.Persistences.EFC.Configuration;
using ModulyBack.Shared.Infraestructure.Persistences.EFC.Repositories;

namespace ModulyBack.Moduly.Infraestructure.Persistence.EFC.Repositories;

public class InvoiceRepository(AppDbContext _context) : BaseRepository<Invoice>(_context), IInvoiceRepository
{
    public new async Task<Invoice?> FindByIdAsync(Guid id) =>
        await _context.Set<Invoice>().FirstOrDefaultAsync(i => i.Id == id);

    public async Task<IEnumerable<Invoice>> ListAsync() =>
        await _context.Set<Invoice>().ToListAsync();

    public async Task<IEnumerable<Invoice>> FindByIssuerIdAsync(Guid issuerId) =>
        await _context.Set<Invoice>().Where(i => i.IssuerId == issuerId).ToListAsync();
}