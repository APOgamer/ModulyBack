namespace ModulyBack.Moduly.Domain.Model.Commands;

public record CreateInvoiceCommand(
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
    Guid UserId);