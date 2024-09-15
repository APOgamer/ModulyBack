using ModulyBack.Moduly.Domain.Model.Commands;
using ModulyBack.Moduly.Domain.Model.Queries;
using ModulyBack.Moduly.Domain.Services;
using ModulyBack.Moduly.Interfaces.REST.Resources;

namespace ModulyBack.Moduly.Interfaces.REST.Transform;

public static class UpdateInvoiceCommandFromResourceAssembler
{
    public static UpdateInvoiceCommand ToCommandFromResource(
        Guid invoiceId,
        UpdateInvoiceResource resource,
        Guid moduleId,           // Se asume que este valor es proporcionado externamente
        Guid? issuerId,         // Se asume que este valor es proporcionado externamente
        DateTime issueDate)     // Se asume que este valor es proporcionado externamente
    {
        // Calcular el TotalPayment
        var totalPayment = resource.Quantity * resource.UnitPrice;

        return new UpdateInvoiceCommand(
            invoiceId,
            resource.Code,
            moduleId,
            issuerId,
            issueDate,
            resource.DueDate,
            resource.Description,
            resource.Quantity,
            resource.UnitPrice,
            totalPayment,
            resource.Status,
            resource.UserCompanyId
        );
    }
}