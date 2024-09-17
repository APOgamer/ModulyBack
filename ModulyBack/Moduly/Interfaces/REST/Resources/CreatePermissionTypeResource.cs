using ModulyBack.Moduly.Domain.Model.ValueObjects;

namespace ModulyBack.Moduly.Interfaces.REST.Resources;

public class CreatePermissionTypeResource
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public Guid CompanyId { get; set; }
    public Guid UserId { get; set; } // Usuario que está creando el permiso
    public List<AllowedActionEnum> AllowedActions { get; set; } = new();
}
