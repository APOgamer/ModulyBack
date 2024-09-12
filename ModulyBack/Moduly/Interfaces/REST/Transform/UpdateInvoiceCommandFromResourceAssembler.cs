using ModulyBack.Moduly.Domain.Model.Commands;
using ModulyBack.Moduly.Domain.Model.Queries;
using ModulyBack.Moduly.Domain.Services;
using ModulyBack.Moduly.Interfaces.REST.Resources;

namespace ModulyBack.Moduly.Interfaces.REST.Transform;

public static class UpdateInvoiceCommandFromResourceAssembler
{
    public static async Task<UpdateInvoiceCommand> ToCommandFromResourceAsync(
        Guid invoiceId,
        UpdateInvoiceResource resource,
        IInvoiceQueryService invoiceQueryService)
    {
        // Obtener la factura actual desde el servicio de consulta
        var invoice = await invoiceQueryService.Handle(new GetInvoiceByIdQuery(invoiceId));
        if (invoice == null)
        {
            throw new ArgumentException("Invoice not found", nameof(invoiceId));
        }

        // Calcular el TotalPayment
        var totalPayment = resource.Quantity * resource.UnitPrice;

        return new UpdateInvoiceCommand(
            invoiceId,
            resource.Code,
            invoice.ModuleId,       // Tomar el valor existente
            invoice.IssuerId,       // Tomar el valor existente
            invoice.IssueDate,      // Tomar el valor existente
            resource.DueDate,
            resource.Description,
            resource.Quantity,
            resource.UnitPrice,
            totalPayment,
            resource.Status
        );
    }
}