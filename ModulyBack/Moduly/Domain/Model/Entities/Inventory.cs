using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ModulyBack.Moduly.Domain.Model.Entities
{
    public class Inventory
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid ModuleId { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        public ICollection<InventoryItem> Items { get; set; } = new List<InventoryItem>();

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }

        public int TotalQuantity => Items.Sum(item => item.Quantity);

        public bool IsInStock => TotalQuantity > 0;

        public void UpdateDetails(string name, string description)
        {
            Name = name;
            Description = description;
            UpdatedAt = DateTime.UtcNow;
        }

        public void AddStock(Guid beingId, int quantity)
        {
            var item = Items.FirstOrDefault(i => i.BeingId == beingId);
            if (item == null)
            {
                item = new InventoryItem { BeingId = beingId, Quantity = quantity };
                Items.Add(item);
            }
            else
            {
                item.Quantity += quantity;
            }
            UpdatedAt = DateTime.UtcNow;
        }

        public void RemoveStock(Guid beingId, int quantity)
        {
            var item = Items.FirstOrDefault(i => i.BeingId == beingId);
            if (item == null || item.Quantity < quantity)
            {
                throw new InvalidOperationException("Not enough stock to remove.");
            }
            item.Quantity -= quantity;
            if (item.Quantity == 0)
            {
                Items.Remove(item);
            }
            UpdatedAt = DateTime.UtcNow;
        }

        public int GetStockLevel(Guid beingId)
        {
            return Items.FirstOrDefault(i => i.BeingId == beingId)?.Quantity ?? 0;
        }
    }


}