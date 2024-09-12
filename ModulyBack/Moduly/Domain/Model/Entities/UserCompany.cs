using ModulyBack.IAM.Domain.Model.Aggregates;
using ModulyBack.Moduly.Domain.Model.Entities;

namespace ModulyBack.Moduly.Domain.Model.Aggregates;

public class UserCompany
{
    public Guid Id { get; set; } // EF Core usa esta propiedad para la clave primaria
    public Guid UserId { get; set; }
    public User User { get; set; } = null!;
    public Guid CompanyId { get; set; }
    public Company Company { get; set; } = null!;
    public string Role { get; set; } = string.Empty;
    public DateTime JoinDate { get; set; } = DateTime.UtcNow;

    public ICollection<ModulePermission> ModulePermissions { get; set; } = new HashSet<ModulePermission>();
}