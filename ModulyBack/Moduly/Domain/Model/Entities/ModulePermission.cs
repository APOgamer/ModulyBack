using System.ComponentModel.DataAnnotations;

namespace ModulyBack.Moduly.Domain.Model.Entities;

public class ModulePermission
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public Guid ModuleId { get; set; }
        
    [Required]
    public Module Module { get; set; } = null!;

    [Required]
    public Guid UserCompanyId { get; set; }
        
    [Required]
    public UserCompany UserCompany { get; set; } = null!;

    public bool CanCreate { get; set; }

    public bool CanView { get; set; } = true;

    public bool CanEdit { get; set; }

    public bool CanDelete { get; set; }
}