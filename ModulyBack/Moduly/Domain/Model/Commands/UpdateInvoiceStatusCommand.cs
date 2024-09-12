namespace ModulyBack.Moduly.Domain.Model.Commands;

public record UpdateInvoiceStatusCommand(Guid InvoiceId, string Status);
