using System.ComponentModel.DataAnnotations;

namespace ModulyBack.Moduly.Domain.Model.Entities
{
    public class Bank
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public decimal TCEA { get; set; }

        public Guid CompanyId { get; set; }
        public Company Company { get; set; } = null!;
    }
}