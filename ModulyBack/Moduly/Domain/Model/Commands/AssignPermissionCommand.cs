using ModulyBack.Moduly.Domain.Model.ValueObjects;

namespace ModulyBack.Moduly.Domain.Model.Commands;

public class AssignPermissionCommand
{
    public Guid AssignerUserId { get; set; }
    public Guid TargetUserId { get; set; }
    public Guid CompanyId { get; set; }
    public Guid ModuleId { get; set; }
    public Guid PermissionTypeId { get; set; }
    public AllowedActionEnum AllowedAction { get; set; }
}