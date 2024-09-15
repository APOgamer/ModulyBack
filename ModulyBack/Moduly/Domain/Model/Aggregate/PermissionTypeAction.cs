using System.ComponentModel.DataAnnotations;
using ModulyBack.Moduly.Domain.Model.ValueObjects;

namespace ModulyBack.Moduly.Domain.Model.Aggregate;

public class PermissionTypeAction
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public Guid PermissionTypeId { get; set; }
    public PermissionType PermissionType { get; set; } = null!;

    [Required]
    public AllowedActionEnum AllowedAction { get; set; }
}
