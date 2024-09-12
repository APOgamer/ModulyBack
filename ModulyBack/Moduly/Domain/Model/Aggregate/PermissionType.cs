using System.ComponentModel.DataAnnotations;
using ModulyBack.Moduly.Domain.Model.Entities;

namespace ModulyBack.Moduly.Domain.Model.Aggregate;

public class PermissionType
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }

    public Guid CompanyId { get; set; }
    public Company Company { get; set; } = null!;
}