using System;
using System.Collections.Generic;
using ModulyBack.Moduly.Domain.Model.Aggregates;

namespace ModulyBack.Moduly.Domain.Model.Entities
{
    public class Invoice
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public Guid ModuleId { get; set; }
        public Module Module { get; set; }
        public Guid? IssuerId { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime? DueDate { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPayment { get; set; }
        public string Status { get; set; }
        public ICollection<Payment> Payments { get; set; } = new List<Payment>();
    }
}