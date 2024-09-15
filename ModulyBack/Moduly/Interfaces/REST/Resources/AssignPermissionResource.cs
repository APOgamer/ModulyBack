using ModulyBack.Moduly.Domain.Model.ValueObjects;

namespace ModulyBack.Moduly.Interfaces.REST.Resources;

public class AssignPermissionResource
{
    public Guid AssignerUserId { get; set; }
    public Guid TargetUserId { get; set; }
    public Guid CompanyId { get; set; }
    public Guid ModuleId { get; set; }
    public Guid PermissionTypeId { get; set; }
    public AllowedActionEnum AllowedAction { get; set; }
}