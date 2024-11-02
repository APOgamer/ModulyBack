using ModulyBack.Moduly.Domain.Model.Aggregate;
using ModulyBack.Moduly.Domain.Model.ValueObjects;

namespace ModulyBack.Moduly.Domain.Model.Commands;

public record CreatePermissionTypeCommand
{
    public string Name { get; init; } = string.Empty;
    public string? Description { get; init; }
    public Guid CompanyId { get; init; }
    public Guid UserId { get; init; }
    public List<AllowedActionEnum> AllowedActions { get; init; } = new();
}