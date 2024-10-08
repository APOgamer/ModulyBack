using Microsoft.EntityFrameworkCore;
using ModulyBack.Moduly.Domain.Model.Entities;
using ModulyBack.Moduly.Domain.Repositories;
using ModulyBack.Shared.Infraestructure.Persistences.EFC.Configuration;
using ModulyBack.Shared.Infraestructure.Persistences.EFC.Repositories;

namespace ModulyBack.Moduly.Infraestructure.Persistence.EFC.Repositories
{
    public class BankRepository : BaseRepository<Bank>, IBankRepository
    {
        public BankRepository(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<Bank>> GetBanksByCompanyIdAsync(Guid companyId)
        {
            return await Context.Banks
                .Where(b => b.CompanyId == companyId)
                .ToListAsync();
        }

        public async Task AddAsync(Bank bank)
        {
            await Context.Banks.AddAsync(bank);
        }

        public async Task UpdateAsync(Bank bank)
        {
            Context.Banks.Update(bank);
            await Context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var bank = await Context.Banks.FindAsync(id);
            if (bank != null)
            {
                Context.Banks.Remove(bank);
                await Context.SaveChangesAsync();
            }
        }
    }
}