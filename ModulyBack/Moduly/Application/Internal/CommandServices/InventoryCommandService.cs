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
            var userCompanyId = await _userCompanyQueryService.FindUserCompanyIdByUserId(command.UserId);
            if (!userCompanyId.HasValue)
                throw new UnauthorizedAccessException("User is not associated with any company.");

            var permission = await _userCompanyPermissionRepository.FindByUserCompanyAndPermissionTypeInModuleAsync(
                userCompanyId.Value, command.ModuleId, AllowedActionEnum.CREATE_INVENTORY);

            if (permission == null)
                throw new UnauthorizedAccessException("User does not have permission to create inventory in this module.");

            var inventory = new Inventory
            {
                Id = Guid.NewGuid(),
                ModuleId = command.ModuleId,
                Quantity = command.InitialStock,
                MinimumStockLevel = command.MinimumStockLevel,
                MaximumStockLevel = command.MaximumStockLevel,
                ReorderPoint = command.ReorderPoint,
                IsInStock = command.InitialStock > 0,
                CreatedAt = DateTime.UtcNow
            };

            await _inventoryRepository.AddAsync(inventory);
            await _unitOfWork.CompleteAsync();

            return inventory;
        }

        public async Task<Inventory> Handle(UpdateInventoryCommand command)
        {
            var inventory = await _inventoryRepository.FindByIdAsync(command.InventoryId);
            if (inventory == null)
                throw new KeyNotFoundException("Inventory not found.");

            var userCompanyId = await _userCompanyQueryService.FindUserCompanyIdByUserId(command.UserId);
            if (!userCompanyId.HasValue)
                throw new UnauthorizedAccessException("User is not associated with any company.");

            var permission = await _userCompanyPermissionRepository.FindByUserCompanyAndPermissionTypeInModuleAsync(
                userCompanyId.Value, inventory.ModuleId, AllowedActionEnum.EDIT_INVENTORY);

            if (permission == null)
                throw new UnauthorizedAccessException("User does not have permission to edit inventory in this module.");

            inventory.UpdateDetails(
                command.Name,
                command.Description,
                command.UnitPrice,
                command.MinimumStockLevel,
                command.MaximumStockLevel,
                command.ReorderPoint
            );

            await _unitOfWork.CompleteAsync();

            return inventory;
        }

        public async Task Handle(DeleteInventoryCommand command)
        {
            var inventory = await _inventoryRepository.FindByIdAsync(command.InventoryId);
            if (inventory == null)
                throw new KeyNotFoundException("Inventory not found.");

            var userCompanyId = await _userCompanyQueryService.FindUserCompanyIdByUserId(command.UserId);
            if (!userCompanyId.HasValue)
                throw new UnauthorizedAccessException("User is not associated with any company.");

            var permission = await _userCompanyPermissionRepository.FindByUserCompanyAndPermissionTypeInModuleAsync(
                userCompanyId.Value, inventory.ModuleId, AllowedActionEnum.DELETE_INVENTORY);

            if (permission == null)
                throw new UnauthorizedAccessException("User does not have permission to delete inventory in this module.");

            _inventoryRepository.Remove(inventory);
            await _unitOfWork.CompleteAsync();
        }

        public async Task<Inventory> Handle(AddStockCommand command)
        {
            var inventory = await _inventoryRepository.FindByIdAsync(command.InventoryId);
            if (inventory == null)
                throw new KeyNotFoundException("Inventory not found.");

            var userCompanyId = await _userCompanyQueryService.FindUserCompanyIdByUserId(command.UserId);
            if (!userCompanyId.HasValue)
                throw new UnauthorizedAccessException("User is not associated with any company.");

            var permission = await _userCompanyPermissionRepository.FindByUserCompanyAndPermissionTypeInModuleAsync(
                userCompanyId.Value, inventory.ModuleId, AllowedActionEnum.EDIT_INVENTORY);

            if (permission == null)
                throw new UnauthorizedAccessException("User does not have permission to edit inventory in this module.");

            inventory.AddStock(command.Amount);
            await _unitOfWork.CompleteAsync();

            return inventory;
        }

        public async Task<Inventory> Handle(RemoveStockCommand command)
        {
            var inventory = await _inventoryRepository.FindByIdAsync(command.InventoryId);
            if (inventory == null)
                throw new KeyNotFoundException("Inventory not found.");

            var userCompanyId = await _userCompanyQueryService.FindUserCompanyIdByUserId(command.UserId);
            if (!userCompanyId.HasValue)
                throw new UnauthorizedAccessException("User is not associated with any company.");

            var permission = await _userCompanyPermissionRepository.FindByUserCompanyAndPermissionTypeInModuleAsync(
                userCompanyId.Value, inventory.ModuleId, AllowedActionEnum.EDIT_INVENTORY);

            if (permission == null)
                throw new UnauthorizedAccessException("User does not have permission to edit inventory in this module.");

            inventory.RemoveStock(command.Amount);
            await _unitOfWork.CompleteAsync();

            return inventory;
        }
    }
}
