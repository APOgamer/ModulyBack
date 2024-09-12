using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ModulyBack.Moduly.Domain.Model.Aggregate;

namespace ModulyBack.Moduly.Domain.Model.Entities
{
    public class Module
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string ModuleName { get; set; } = string.Empty;

        [Required]
        public string ModuleType { get; set; } = string.Empty;

        [Required]
        public Guid CompanyId { get; set; }
        
        public Company Company { get; set; }

        [Required]
        public DateTime CreationDate { get; set; } = DateTime.UtcNow;

        public ICollection<ModulePermission> Permissions { get; set; } = new List<ModulePermission>();
        public ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();
        public ICollection<BeingModule> BeingModules { get; set; } = new List<BeingModule>();
        public ICollection<ModulePermission> ModulePermissions { get; set; } = new List<ModulePermission>();

    }
}