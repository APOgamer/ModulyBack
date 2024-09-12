using ModulyBack.Moduly.Domain.Model.Entities;
using ModulyBack.Moduly.Domain.Model.Queries;

namespace ModulyBack.Moduly.Domain.Services;

public interface IInvoiceQueryService
{
    Task<Invoice?> Handle(GetInvoiceByIdQuery query);
    Task<IEnumerable<Invoice>> Handle(GetAllInvoicesQuery query);
}