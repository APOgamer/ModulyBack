using ModulyBack.Moduly.Domain.Model.Entities;
using ModulyBack.Moduly.Domain.Model.Queries;
using ModulyBack.Moduly.Domain.Repositories;
using ModulyBack.Moduly.Domain.Services;

namespace ModulyBack.Moduly.Application.Internal.QueryServices
{
    public class InventoryQueryService : IInventoryQueryService
    {
        private readonly IInventoryRepository _inventoryRepository;

        public InventoryQueryService(IInventoryRepository inventoryRepository)
        {
            _inventoryRepository = inventoryRepository;
        }

        public async Task<Inventory?> Handle(GetInventoryByIdQuery query)
        {
            return await _inventoryRepository.FindByIdAsync(query.InventoryId);
        }

        public async Task<IEnumerable<Inventory>> Handle(GetInventoriesByModuleQuery query)
        {
            return await _inventoryRepository.FindByModuleIdAsync(query.ModuleId);
        }
    }
}