﻿using System.ComponentModel.DataAnnotations;
using ModulyBack.Moduly.Domain.Model.Aggregate;

namespace ModulyBack.Moduly.Domain.Model.Entities;

public class ModulePermission
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public Guid ModuleId { get; set; }
    public Module Module { get; set; } = null!;

    [Required]
    public Guid PermissionTypeId { get; set; }
    public PermissionType PermissionType { get; set; } = null!;

    public bool IsAvailable { get; set; }
}