﻿using System.ComponentModel.DataAnnotations;
using ModulyBack.IAM.Domain.Model.Aggregates;
using ModulyBack.Moduly.Domain.Model.Aggregate;

namespace ModulyBack.Moduly.Domain.Model.Entities;

public class UserCompany
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public Guid UserId { get; set; }
        
    [Required]
    public User User { get; set; } = null!;

    [Required]
    public Guid CompanyId { get; set; }
        
    [Required]
    public Company Company { get; set; } = null!;

    [Required]
    public string Role { get; set; } = string.Empty;

    [Required]
    public DateTime JoinDate { get; set; } = DateTime.UtcNow;
    public ICollection<UserCompanyPermission> UserCompanyPermissions { get; set; } = new HashSet<UserCompanyPermission>();

    
}