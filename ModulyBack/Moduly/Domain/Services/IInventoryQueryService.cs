// IInventoryQueryService
using ModulyBack.Moduly.Domain.Model.Entities;
using ModulyBack.Moduly.Domain.Model.Queries;

namespace ModulyBack.Moduly.Domain.Services;

public interface IInventoryQueryService
{
    Task<Inventory?> Handle(GetInventoryByIdQuery query);
    Task<IEnumerable<Inventory>> Handle(GetInventoriesByModuleQuery query);
}