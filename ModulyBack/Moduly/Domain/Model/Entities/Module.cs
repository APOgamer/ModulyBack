using System;
using System.Collections.Generic;
using ModulyBack.Moduly.Domain.Model.Aggregates;

namespace ModulyBack.Moduly.Domain.Model.Entities
{
    public class Module
    {
        public Guid Id { get; set; }
        public string ModuleName { get; set; }
        public string ModuleType { get; set; }
        public Guid CompanyId { get; set; }
        public Company Company { get; set; }
        public DateTime CreationDate { get; set; }
        public ICollection<ModulePermission> Permissions { get; set; } = new List<ModulePermission>();
        public ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();
    }
}