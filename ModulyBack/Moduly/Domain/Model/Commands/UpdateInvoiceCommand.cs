namespace ModulyBack.Moduly.Domain.Model.Commands;

public record UpdateInvoiceCommand(
    Guid Id,
    string Code,
    Guid ModuleId,
    Guid? IssuerId,
    DateTime IssueDate,
    DateTime? DueDate,
    string Description,
    int Quantity,
    decimal UnitPrice,
    decimal TotalPayment,
    string Status,
    Guid UserCompanyId
);

