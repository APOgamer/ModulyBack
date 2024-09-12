using System.ComponentModel.DataAnnotations;

namespace ModulyBack.Moduly.Domain.Model.Entities;

public class Payment
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public Guid InvoiceId { get; set; }
        
    [Required]
    public Invoice Invoice { get; set; } = null!;

    [Required]
    [Range(0, double.MaxValue)] // Asegura que Amount sea no negativo
    public decimal Amount { get; set; }

    [Required]
    public DateTime PaymentDate { get; set; } = DateTime.UtcNow;

    public string? PaymentMethod { get; set; }
}