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

        public Guid? IssuerId { get; set; }

        public DateTime IssueDate { get; set; }

        public DateTime? DueDate { get; set; }

        public string Description { get; set; } = string.Empty;

        [Required]
        public int Quantity { get; set; }

        [Required]
        [Range(0, double.MaxValue)] // Validación para asegurarse de que UnitPrice sea no negativo
        public decimal UnitPrice { get; set; }

        [Required]
        [Range(0, double.MaxValue)] // Validación para asegurarse de que TotalPayment sea no negativo
        public decimal TotalPayment { get; set; }

        [Required]
        public string Status { get; set; } = string.Empty;

        public ICollection<Payment> Payments { get; set; } = new List<Payment>();
    }
}