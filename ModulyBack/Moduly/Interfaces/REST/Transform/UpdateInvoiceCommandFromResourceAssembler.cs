using ModulyBack.Moduly.Domain.Model.Commands;
using ModulyBack.Moduly.Interfaces.REST.Resources;

namespace ModulyBack.Moduly.Interfaces.REST.Transform;

public static class UpdateInvoiceCommandFromResourceAssembler
{
    public static UpdateInvoiceCommand ToCommandFromResource(
        Guid invoiceId,
        UpdateInvoiceResource resource,
        Guid moduleId,
        Guid? issuerId,
        DateTime issueDate)
    {
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
            resource.UserCompanyId,
            resource.ExchangeRate,
            resource.DiscountDate,
            resource.TCEA,
            resource.BankId
            
        );
    }
}