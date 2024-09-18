using System;
using System.ComponentModel.DataAnnotations;

namespace ModulyBack.Moduly.Domain.Model.Entities
{
    public class Inventory
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid ModuleId { get; set; }

        [Required]
        public Guid BeingId { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public int MinimumStockLevel { get; set; }

        [Required]
        public int MaximumStockLevel { get; set; }

        [Required]
        public int ReorderPoint { get; set; }

        [Required]
        public bool IsInStock { get; set; }

        public DateTime? LastRestockDate { get; set; }

        public DateTime? LastSaleDate { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public int QuantityForReposition => MaximumStockLevel - Quantity;

        public void UpdateStockStatus()
        {
            IsInStock = Quantity > 0;
        }

        public void AddStock(int amount)
        {
            Quantity += amount;
            LastRestockDate = DateTime.UtcNow;
            UpdateStockStatus();
        }

        public void RemoveStock(int amount)
        {
            if (amount > Quantity)
            {
                throw new InvalidOperationException("Cannot remove more stock than available.");
            }
            Quantity -= amount;
            LastSaleDate = DateTime.UtcNow;
            UpdateStockStatus();
        }
    }
}