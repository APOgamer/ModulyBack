using ModulyBack.Moduly.Domain.Model.Entities;

namespace ModulyBack.Moduly.Domain.Model.Aggregate;

public class BeingModule
{
    public Guid BeingId { get; set; }
    public Being Being { get; set; } = null!;

    public Guid ModuleId { get; set; }
    public Module Module { get; set; } = null!;
}
