using System.ComponentModel.DataAnnotations;
using ModulyBack.Moduly.Domain.Model.Entities;

namespace ModulyBack.Moduly.Domain.Model.Aggregate;

public class UserCompanyPermission
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public Guid UserCompanyId { get; set; }
    public UserCompany UserCompany { get; set; } = null!;

    [Required]
    public Guid ModuleId { get; set; }
    public Module Module { get; set; } = null!;

    [Required]
    public Guid PermissionTypeId { get; set; }
    public PermissionType PermissionType { get; set; } = null!;

    public bool IsGranted { get; set; }
}