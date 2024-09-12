using ModulyBack.Moduly.Domain.Model.Entities;
using ModulyBack.Moduly.Domain.Model.Queries;
using ModulyBack.Moduly.Domain.Repositories;
using ModulyBack.Moduly.Domain.Services;

namespace ModulyBack.Moduly.Application.Internal.QueryServices
{
    public class InvoiceQueryService : IInvoiceQueryService
    {
        private readonly IInvoiceRepository _invoiceRepository;

        public InvoiceQueryService(IInvoiceRepository invoiceRepository)
        {
            _invoiceRepository = invoiceRepository;
        }

        public async Task<Invoice?> Handle(GetInvoiceByIdQuery query)
        {
            return await _invoiceRepository.FindByIdAsync(query.Id);
        }

        public async Task<IEnumerable<Invoice>> Handle(GetAllInvoicesQuery query)
        {
            return await _invoiceRepository.ListAsync();
        }
    }
}