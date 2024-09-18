using Microsoft.AspNetCore.Mvc;
using ModulyBack.Moduly.Domain.Model.Commands;
using ModulyBack.Moduly.Domain.Model.Queries;
using ModulyBack.Moduly.Domain.Services;
using ModulyBack.Moduly.Interfaces.REST.Resources;
using ModulyBack.Moduly.Interfaces.REST.Transform;
using ModulyBack.Moduly.Domain.Model.ValueObjects;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using ModulyBack.Moduly.Domain.Repositories;
using ModulyBack.Moduly.Domain.Model.Entities;

namespace ModulyBack.Moduly.Interfaces.REST
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class InventoryController : ControllerBase
    {
        private readonly IInventoryCommandService _inventoryCommandService;
        private readonly IInventoryQueryService _inventoryQueryService;
        private readonly IUserCompanyQueryService _userCompanyQueryService;
        private readonly IUserCompanyPermissionRepository _userCompanyPermissionRepository;
        private readonly IBeingQueryService _beingQueryService; 

        public InventoryController(
            IInventoryCommandService inventoryCommandService,
            IInventoryQueryService inventoryQueryService,
            IUserCompanyQueryService userCompanyQueryService,
            IUserCompanyPermissionRepository userCompanyPermissionRepository,
            IBeingQueryService beingQueryService) 
        {
            _inventoryCommandService = inventoryCommandService;
            _inventoryQueryService = inventoryQueryService;
            _userCompanyQueryService = userCompanyQueryService;
            _userCompanyPermissionRepository = userCompanyPermissionRepository;
            _beingQueryService = beingQueryService; 
        }
    


        [HttpPost]
        public async Task<IActionResult> CreateInventory([FromBody] CreateInventoryResource createInventoryResource)
        {
            try
            {
                var userCompanyId = await _userCompanyQueryService.FindUserCompanyIdByUserId(createInventoryResource.UserId);
                if (!userCompanyId.HasValue)
                {
                    return Forbid("User is not associated with any company.");
                }

                var userPermission = await _userCompanyPermissionRepository
                    .FindByUserCompanyAndPermissionTypeInModuleAsync(userCompanyId.Value, createInventoryResource.ModuleId, AllowedActionEnum.CREATE_INVENTORY);

                if (userPermission == null)
                    return Forbid($"User does not have permission to create inventory in ModuleId: {createInventoryResource.ModuleId}");

                var createInventoryCommand = CreateInventoryCommandFromResourceAssembler.ToCommandFromResource(createInventoryResource);
                var inventory = await _inventoryCommandService.Handle(createInventoryCommand);

                var resource = InventoryResourceFromEntityAssembler.ToResourceFromEntity(inventory);
                return CreatedAtAction(nameof(GetInventoryById), new { inventoryId = resource.Id }, resource);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while creating the inventory: {ex.Message}");
            }
        }

        [HttpPut("{inventoryId:guid}")]
        public async Task<IActionResult> UpdateInventory([FromRoute] Guid inventoryId, [FromBody] UpdateInventoryResource updateInventoryResource)
        {
            try
            {
                var userCompanyId = await _userCompanyQueryService.FindUserCompanyIdByUserId(updateInventoryResource.UserId);
                if (!userCompanyId.HasValue)
                {
                    return Forbid("User is not associated with any company.");
                }

                var inventory = await _inventoryQueryService.Handle(new GetInventoryByIdQuery(inventoryId));
                if (inventory == null) return NotFound();

                var userPermission = await _userCompanyPermissionRepository
                    .FindByUserCompanyAndPermissionTypeInModuleAsync(userCompanyId.Value, inventory.ModuleId, AllowedActionEnum.EDIT_INVENTORY);

                if (userPermission == null)
                    return Forbid("User does not have permission to edit inventory in this module.");

                var updateInventoryCommand = UpdateInventoryCommandFromResourceAssembler.ToCommandFromResource(inventoryId, updateInventoryResource);
                await _inventoryCommandService.Handle(updateInventoryCommand);

                var updatedInventory = await _inventoryQueryService.Handle(new GetInventoryByIdQuery(inventoryId));
                var resource = InventoryResourceFromEntityAssembler.ToResourceFromEntity(updatedInventory);
                return Ok(resource);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while updating the inventory: {ex.Message}");
            }
        }

        [HttpDelete("{inventoryId:guid}")]
        public async Task<IActionResult> DeleteInventory([FromRoute] Guid inventoryId, [FromQuery] Guid userId)
        {
            try
            {
                var userCompanyId = await _userCompanyQueryService.FindUserCompanyIdByUserId(userId);
                if (!userCompanyId.HasValue)
                {
                    return Forbid("User is not associated with any company.");
                }

                var inventory = await _inventoryQueryService.Handle(new GetInventoryByIdQuery(inventoryId));
                if (inventory == null) return NotFound();

                var userPermission = await _userCompanyPermissionRepository
                    .FindByUserCompanyAndPermissionTypeInModuleAsync(userCompanyId.Value, inventory.ModuleId, AllowedActionEnum.DELETE_INVENTORY);

                if (userPermission == null)
                    return Forbid("User does not have permission to delete inventory in this module.");

                var deleteInventoryCommand = new DeleteInventoryCommand(inventoryId, userCompanyId.Value);
                await _inventoryCommandService.Handle(deleteInventoryCommand);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while deleting the inventory: {ex.Message}");
            }
        }

        [HttpGet("{inventoryId:guid}")]
        public async Task<IActionResult> GetInventoryById([FromRoute] Guid inventoryId, [FromQuery] Guid userId)
        {
            try
            {
                var userCompanyId = await _userCompanyQueryService.FindUserCompanyIdByUserId(userId);
                if (!userCompanyId.HasValue)
                {
                    return Forbid("User is not associated with any company.");
                }

                var inventory = await _inventoryQueryService.Handle(new GetInventoryByIdQuery(inventoryId));
                if (inventory == null) return NotFound();

                var userPermission = await _userCompanyPermissionRepository
                    .FindByUserCompanyAndPermissionTypeInModuleAsync(userCompanyId.Value, inventory.ModuleId, AllowedActionEnum.VIEW_INVENTORY);

                if (userPermission == null)
                    return Forbid("User does not have permission to view inventory in this module.");

                var resource = InventoryResourceFromEntityAssembler.ToResourceFromEntity(inventory);
                return Ok(resource);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving the inventory: {ex.Message}");
            }
        }

        [HttpGet("byModule/{moduleId:guid}")]
        public async Task<IActionResult> GetInventoriesByModule([FromRoute] Guid moduleId, [FromQuery] Guid userId)
        {
            try
            {
                var userCompanyId = await _userCompanyQueryService.FindUserCompanyIdByUserId(userId);
                if (!userCompanyId.HasValue)
                {
                    return Forbid("User is not associated with any company.");
                }

                var userPermission = await _userCompanyPermissionRepository
                    .FindByUserCompanyAndPermissionTypeInModuleAsync(userCompanyId.Value, moduleId, AllowedActionEnum.VIEW_INVENTORY);

                if (userPermission == null)
                    return Forbid("User does not have permission to view inventory in this module.");

                var inventories = await _inventoryQueryService.Handle(new GetInventoriesByModuleQuery(moduleId));
                var resources = inventories.Select(InventoryResourceFromEntityAssembler.ToResourceFromEntity);
                return Ok(resources);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving inventories: {ex.Message}");
            }
        }

        [HttpPut("{inventoryId:guid}/addStock")]
        public async Task<IActionResult> AddStock([FromRoute] Guid inventoryId, [FromBody] AddStockResource addStockResource)
        {
            try
            {
                var userCompanyId = await _userCompanyQueryService.FindUserCompanyIdByUserId(addStockResource.UserId);
                if (!userCompanyId.HasValue)
                {
                    return Forbid("User is not associated with any company.");
                }

                var inventory = await _inventoryQueryService.Handle(new GetInventoryByIdQuery(inventoryId));
                if (inventory == null) return NotFound();

                var userPermission = await _userCompanyPermissionRepository
                    .FindByUserCompanyAndPermissionTypeInModuleAsync(userCompanyId.Value, inventory.ModuleId, AllowedActionEnum.EDIT_INVENTORY);

                if (userPermission == null)
                    return Forbid("User does not have permission to edit inventory in this module.");

                // Use the new query to check if the being is associated with the module
                var beingModule = await _beingQueryService.Handle(new CheckBeingModuleQuery(addStockResource.BeingId, inventory.ModuleId));
                if (beingModule == null)
                    return BadRequest("The specified being is not associated with the module.");

                var addStockCommand = new AddStockCommand(inventoryId, addStockResource.BeingId, addStockResource.Quantity, userCompanyId.Value);
                await _inventoryCommandService.Handle(addStockCommand);

                var updatedInventory = await _inventoryQueryService.Handle(new GetInventoryByIdQuery(inventoryId));
                var resource = InventoryResourceFromEntityAssembler.ToResourceFromEntity(updatedInventory);
                return Ok(resource);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while adding stock: {ex.Message}");
            }
        }
        [HttpPut("{inventoryId:guid}/removeStock")]
        public async Task<IActionResult> RemoveStock([FromRoute] Guid inventoryId, [FromBody] RemoveStockResource removeStockResource)
        {
            try
            {
                var userCompanyId = await _userCompanyQueryService.FindUserCompanyIdByUserId(removeStockResource.UserId);
                if (!userCompanyId.HasValue)
                {
                    return Forbid("User is not associated with any company.");
                }

                var inventory = await _inventoryQueryService.Handle(new GetInventoryByIdQuery(inventoryId));
                if (inventory == null) return NotFound();

                var userPermission = await _userCompanyPermissionRepository
                    .FindByUserCompanyAndPermissionTypeInModuleAsync(userCompanyId.Value, inventory.ModuleId, AllowedActionEnum.EDIT_INVENTORY);

                if (userPermission == null)
                    return Forbid("User does not have permission to edit inventory in this module.");

                // Use the new query to check if the being is associated with the module
                var beingModule = await _beingQueryService.Handle(new CheckBeingModuleQuery(removeStockResource.BeingId, inventory.ModuleId));
                if (beingModule == null)
                    return BadRequest("The specified being is not associated with the module.");

                var removeStockCommand = new RemoveStockCommand(inventoryId, removeStockResource.BeingId, removeStockResource.Quantity, userCompanyId.Value);
                await _inventoryCommandService.Handle(removeStockCommand);

                var updatedInventory = await _inventoryQueryService.Handle(new GetInventoryByIdQuery(inventoryId));
                var resource = InventoryResourceFromEntityAssembler.ToResourceFromEntity(updatedInventory);
                return Ok(resource);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while removing stock: {ex.Message}");
            }
        }



    }
}