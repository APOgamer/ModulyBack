using ModulyBack.Moduly.Domain.Model.Commands;
using ModulyBack.Moduly.Interfaces.REST.Resources;

namespace ModulyBack.Moduly.Interfaces.REST.Transform;

public static class CreateInvoiceCommandFromResourceAssembler
{
    public static CreateInvoiceCommand ToCommandFromResource(CreateInvoiceResource resource)
    {
        // Calcular el TotalPayment antes de invocar el constructor
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
            totalPayment, // Se añade este valor para que coincidan los parámetros
            resource.Status,
            resource.UserId
            );
    }
}