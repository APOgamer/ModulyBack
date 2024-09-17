using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ModulyBack.Moduly.Domain.Model.Entities
{
    public class Invoice
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Code { get; set; } = string.Empty;

        [Required]
        public Guid ModuleId { get; set; }
        
        public Module Module { get; set; }

        public Guid? IssuerId { get; set; } // El emisor puede ser opcional en algunos casos

        [Required]
        public DateTime IssueDate { get; set; }

        public DateTime? DueDate { get; set; }

        [Required]
        public string Description { get; set; } = string.Empty;

    
        public int Quantity { get; set; }
        
        [Range(0, double.MaxValue)] // Validación para asegurarse de que UnitPrice sea no negativo
        public decimal UnitPrice { get; set; }

     
        [Range(0, double.MaxValue)] // Validación para asegurarse de que TotalPayment sea no negativo
        public decimal TotalPayment { get; set; }

        [Required]
        public string Status { get; set; } = string.Empty;

        // Campos adicionales según el enunciado
        public string Currency { get; set; } = "USD"; // Moneda, por defecto USD

        public decimal? ExchangeRate { get; set; } // Tipo de cambio si se usa una moneda diferente

        public DateTime? DiscountDate { get; set; } // Fecha de descuento

        public decimal? TCEA { get; set; } // Tasa de Coste Efectivo Anual (TCEA), calculada

        public ICollection<Payment> Payments { get; set; } = new List<Payment>();
    }
}