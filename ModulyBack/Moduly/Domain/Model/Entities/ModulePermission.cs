using ModulyBack.Moduly.Domain.Model.Entities;

namespace ModulyBack.Moduly.Domain.Model.Aggregates;

public class ModulePermission
{
    public Guid Id { get; set; } // EF Core usa esta propiedad para la clave primaria
    public Guid ModuleId { get; set; }
    public Module Module { get; set; } = null!;
    public Guid UserCompanyId { get; set; }
    public UserCompany UserCompany { get; set; } = null!;
    public bool CanCreate { get; set; }
    public bool CanView { get; set; } = true;
    public bool CanEdit { get; set; }
    public bool CanDelete { get; set; }
}