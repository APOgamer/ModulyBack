using ModulyBack.Moduly.Domain.Model.Commands;
using ModulyBack.Moduly.Interfaces.REST.Resources;

namespace ModulyBack.Moduly.Interfaces.REST.Transform;

public static class CreateInvoiceCommandFromResourceAssembler
{
    public static CreateInvoiceCommand ToCommandFromResource(CreateInvoiceResource resource)
    {
        var totalPayment = resource.Quantity * resource.UnitPrice;

        return new CreateInvoiceCommand(
            resource.Code,
            resource.ModuleId,
            resource.IssuerId,
            resource.IssueDate,
            resource.DueDate,
            resource.Description,
            resource.Quantity,
            resource.UnitPrice,
            totalPayment,
            resource.Status,
            resource.UserId,
            resource.ExchangeRate,
            resource.DiscountDate,
            resource.TCEA
        );
    }
}