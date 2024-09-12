using ModulyBack.Moduly.Domain.Model.Entities;
using ModulyBack.Moduly.Interfaces.REST.Resources;

namespace ModulyBack.Moduly.Interfaces.REST.Transform;

public static class InvoiceResourceFromEntityAssembler
{
    public static InvoiceResource ToResourceFromEntity(Invoice invoice)
    {
        return new InvoiceResource
        {
            Id = invoice.Id,
            Code = invoice.Code,
            ModuleId = invoice.ModuleId,
            IssuerId = invoice.IssuerId,
            IssueDate = invoice.IssueDate,
            DueDate = invoice.DueDate,
            Description = invoice.Description,
            Quantity = invoice.Quantity,
            UnitPrice = invoice.UnitPrice,
            TotalPayment = invoice.TotalPayment,
            Status = invoice.Status
        };
    }
}