using ModulyBack.Moduly.Domain.Model.Entities;
using ModulyBack.Shared.Domain.Repositories;

namespace ModulyBack.Moduly.Domain.Repositories
{
    public interface IInvoiceRepository : IBaseRepository<Invoice>
    {
        Task<Invoice?> FindByIdAsync(Guid id);
        Task<IEnumerable<Invoice>> ListAsync();
        Task<IEnumerable<Invoice>> FindByIssuerIdAsync(Guid issuerId);
    }
}