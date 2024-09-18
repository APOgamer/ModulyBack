using System;
using System.Threading.Tasks;
using ModulyBack.Moduly.Domain.Model.Commands;
using ModulyBack.Moduly.Domain.Model.Entities;
using ModulyBack.Moduly.Domain.Model.ValueObjects;
using ModulyBack.Moduly.Domain.Repositories;
using ModulyBack.Moduly.Domain.Services;
using ModulyBack.Shared.Domain.Repositories;

namespace ModulyBack.Moduly.Application.Internal.CommandServices
{
    public class InventoryCommandService : IInventoryCommandService
    {
        private readonly IInventoryRepository _inventoryRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserCompanyPermissionRepository _userCompanyPermissionRepository;
        private readonly IUserCompanyQueryService _userCompanyQueryService;

        public InventoryCommandService(
            IInventoryRepository inventoryRepository,
            IUnitOfWork unitOfWork,
            IUserCompanyPermissionRepository userCompanyPermissionRepository,
            IUserCompanyQueryService userCompanyQueryService)
        {
            _inventoryRepository = inventoryRepository;
            _unitOfWork = unitOfWork;
            _userCompanyPermissionRepository = userCompanyPermissionRepository;
            _userCompanyQueryService = userCompanyQueryService;
        }

        public async Task<Inventory> Handle(CreateInventoryCommand command)
        {
            await ValidateUserPermission(command.UserId, command.ModuleId, AllowedActionEnum.CREATE_INVENTORY);

            var inventory = new Inventory
            {
                Id = Guid.NewGuid(),
                ModuleId = command.ModuleId,
                Name = command.Name,
                Description = command.Description,
                CreatedAt = DateTime.UtcNow
            };

            await _inventoryRepository.AddAsync(inventory);
            await _unitOfWork.CompleteAsync();

            return inventory;
        }

        public async Task<Inventory> Handle(UpdateInventoryCommand command)
        {
            var inventory = await GetInventory(command.InventoryId);
            await ValidateUserPermission(command.UserId, inventory.ModuleId, AllowedActionEnum.EDIT_INVENTORY);

            inventory.UpdateDetails(command.Name, command.Description);
            await _unitOfWork.CompleteAsync();

            return inventory;
        }

        public async Task Handle(DeleteInventoryCommand command)
        {
            var inventory = await GetInventory(command.InventoryId);
            await ValidateUserPermission(command.UserId, inventory.ModuleId, AllowedActionEnum.DELETE_INVENTORY);

            _inventoryRepository.Remove(inventory);
            await _unitOfWork.CompleteAsync();
        }

        public async Task<Inventory> Handle(AddStockCommand command)
        {
            var inventory = await GetInventory(command.InventoryId);
            await ValidateUserPermission(command.UserId, inventory.ModuleId, AllowedActionEnum.EDIT_INVENTORY);

            inventory.AddStock(command.BeingId, command.Quantity);
            await _unitOfWork.CompleteAsync();

            return inventory;
        }

        public async Task<Inventory> Handle(RemoveStockCommand command)
        {
            var inventory = await GetInventory(command.InventoryId);
            await ValidateUserPermission(command.UserId, inventory.ModuleId, AllowedActionEnum.EDIT_INVENTORY);

            inventory.RemoveStock(command.BeingId, command.Quantity);
            await _unitOfWork.CompleteAsync();

            return inventory;
        }

        private async Task<Inventory> GetInventory(Guid inventoryId)
        {
            var inventory = await _inventoryRepository.FindByIdAsync(inventoryId);
            if (inventory == null)
                throw new KeyNotFoundException("Inventory not found.");

            return inventory;
        }

        private async Task ValidateUserPermission(Guid userId, Guid moduleId, AllowedActionEnum action)
        {
            var userCompanyId = await _userCompanyQueryService.FindUserCompanyIdByUserId(userId);
            if (!userCompanyId.HasValue)
                throw new UnauthorizedAccessException("User is not associated with any company.");

            var permission = await _userCompanyPermissionRepository.FindByUserCompanyAndPermissionTypeInModuleAsync(
                userCompanyId.Value, moduleId, action);

            if (permission == null)
                throw new UnauthorizedAccessException($"User does not have permission to perform '{action}' in this module.");
        }
    }
}
