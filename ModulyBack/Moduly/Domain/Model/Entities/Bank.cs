using System;
using System.ComponentModel.DataAnnotations;

namespace ModulyBack.Moduly.Domain.Model.Entities
{
    public class Bank
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public Guid CompanyId { get; set; }

        public string? AccountNumber { get; set; }
        public string? IBAN { get; set; }
        public string? SWIFT { get; set; }
        public string? AccountHolderName { get; set; }
        public string? AccountType { get; set; }
        public string? BankAddress { get; set; }
        public string? PaymentReference { get; set; }
        
        public float? TCEApreferredRate { get; set; }

        // Navegación a la compañía
        public Company Company { get; set; }
        
        public Guid? BankId { get; set; }
    }
}