using ModulyBack.Moduly.Domain.Model.Entities;

namespace ModulyBack.Moduly.Domain.Model.Aggregates;

public class Payment
{
    public Guid Id { get; set; } // EF Core usa esta propiedad para la clave primaria
    public Guid InvoiceId { get; set; }
    public Invoice Invoice { get; set; } = null!;
    public decimal Amount { get; set; }
    public DateTime PaymentDate { get; set; } = DateTime.UtcNow;
    public string? PaymentMethod { get; set; }
}