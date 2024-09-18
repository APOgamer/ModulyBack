using Microsoft.AspNetCore.Mvc;
using ModulyBack.Moduly.Domain.Model.Commands;
using ModulyBack.Moduly.Domain.Model.Queries;
using ModulyBack.Moduly.Domain.Services;
using ModulyBack.Moduly.Interfaces.REST.Resources;
using ModulyBack.Moduly.Interfaces.REST.Transform;
using ModulyBack.Moduly.Domain.Model.ValueObjects;
using ModulyBack.Moduly.Domain.Repositories;

namespace ModulyBack.Moduly.Interfaces.REST
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class BeingController : ControllerBase
    {
        private readonly IBeingCommandService _beingCommandService;
        private readonly IBeingQueryService _beingQueryService;
        private readonly IUserCompanyQueryService _userCompanyQueryService;
        private readonly ICompanyQueryService _companyQueryService;
        private readonly IUserCompanyPermissionRepository _userCompanyPermissionRepository;

        public BeingController(
            IBeingCommandService beingCommandService,
            IBeingQueryService beingQueryService,
            IUserCompanyQueryService userCompanyQueryService,
            ICompanyQueryService companyQueryService,
            IUserCompanyPermissionRepository userCompanyPermissionRepository)
        {
            _beingCommandService = beingCommandService;
            _beingQueryService = beingQueryService;
            _userCompanyQueryService = userCompanyQueryService;
            _companyQueryService = companyQueryService;
            _userCompanyPermissionRepository = userCompanyPermissionRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateBeing([FromBody] CreateBeingResource createBeingResource)
        {
            var createBeingCommand = CreateBeingCommandFromResourceAssembler.ToCommandFromResource(createBeingResource);

            try
            {
                var userCompanyId = await _userCompanyQueryService.FindUserCompanyIdByUserId(createBeingCommand.UserId);
                if (!userCompanyId.HasValue)
                {
                    return Forbid("User is not associated with any company.");
                }

                var company = await _companyQueryService.Handle(new GetCompanyByModuleIdQuery(createBeingCommand.ModuleId));
                if (company != null && company.CreatedById == createBeingCommand.UserId)
                {
                    var creatorBeing = await _beingCommandService.Handle(createBeingCommand);
                    var creatorResource = BeingResourceFromEntityAssembler.ToResourceFromEntity(creatorBeing);
                    return CreatedAtAction(nameof(GetBeingById), new { beingId = creatorResource.Id }, creatorResource);
                }

                var userPermission = await _userCompanyPermissionRepository
                    .FindByUserCompanyAndPermissionTypeInModuleAsync(userCompanyId.Value, createBeingCommand.ModuleId, AllowedActionEnum.CREATE_BEING);

                if (userPermission == null)
                    return Forbid($"User does not have permission to create beings in ModuleId: {createBeingCommand.ModuleId}");

                var regularBeing = await _beingCommandService.Handle(createBeingCommand);

                if (regularBeing is null) return BadRequest();

                var regularResource = BeingResourceFromEntityAssembler.ToResourceFromEntity(regularBeing);
                return CreatedAtAction(nameof(GetBeingById), new { beingId = regularResource.Id }, regularResource);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Forbid(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while creating the being: {ex.Message}");
            }
        }

        [HttpGet("{beingId:guid}")]
        public async Task<IActionResult> GetBeingById([FromRoute] Guid beingId, [FromQuery] Guid userId)
        {
            try
            {
                var userCompanyId = await _userCompanyQueryService.FindUserCompanyIdByUserId(userId);
                if (!userCompanyId.HasValue)
                {
                    return Forbid("User is not associated with any company.");
                }

                var being = await _beingQueryService.Handle(new GetBeingByIdQuery(beingId));
                if (being is null) return NotFound();

                var userPermission = await _userCompanyPermissionRepository
                    .FindByUserCompanyAndPermissionTypeInModuleAsync(userCompanyId.Value, being.BeingModules.First().ModuleId, AllowedActionEnum.VIEW_BEING);

                if (userPermission == null)
                    return Forbid($"User does not have permission to view beings in this module.");

                var resource = BeingResourceFromEntityAssembler.ToResourceFromEntity(being);
                return Ok(resource);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving the being: {ex.Message}");
            }
        }

        [HttpGet("byModule/{moduleId:guid}")]
        public async Task<IActionResult> GetBeingsByModule([FromRoute] Guid moduleId, [FromQuery] Guid userId)
        {
            try
            {
                var userCompanyId = await _userCompanyQueryService.FindUserCompanyIdByUserId(userId);
                if (!userCompanyId.HasValue)
                {
                    return Forbid("User is not associated with any company.");
                }

                var userPermission = await _userCompanyPermissionRepository
                    .FindByUserCompanyAndPermissionTypeInModuleAsync(userCompanyId.Value, moduleId, AllowedActionEnum.VIEW_BEING);

                if (userPermission == null)
                    return Forbid($"User does not have permission to view beings in this module.");

                var query = new GetBeingsByModuleQuery(moduleId);
                var beings = await _beingQueryService.Handle(query);
                var resources = beings.Select(BeingResourceFromEntityAssembler.ToResourceFromEntity);
                return Ok(resources);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving beings: {ex.Message}");
            }
        }

        [HttpPut("{beingId:guid}")]
        public async Task<IActionResult> UpdateBeing([FromRoute] Guid beingId, [FromBody] UpdateBeingResource updateBeingResource)
        {
            try
            {
                var being = await _beingQueryService.Handle(new GetBeingByIdQuery(beingId));
                if (being is null) return NotFound();

                var userCompanyId = await _userCompanyQueryService.FindUserCompanyIdByUserId(updateBeingResource.UserId);
                if (!userCompanyId.HasValue)
                {
                    return Forbid("User is not associated with any company.");
                }

                var userPermission = await _userCompanyPermissionRepository
                    .FindByUserCompanyAndPermissionTypeInModuleAsync(userCompanyId.Value, being.BeingModules.First().ModuleId, AllowedActionEnum.EDIT_BEING);

                if (userPermission == null)
                    return Forbid("User does not have permission to edit beings in this module.");

                var updateBeingCommand = UpdateBeingCommandFromResourceAssembler.ToCommandFromResource(
                    beingId,
                    updateBeingResource,
                    being.BeingModules.First().ModuleId,
                    updateBeingResource.UserId
                );

                await _beingCommandService.Handle(updateBeingCommand);

                var updatedBeing = await _beingQueryService.Handle(new GetBeingByIdQuery(beingId));
                if (updatedBeing is null) return NotFound();

                var resource = BeingResourceFromEntityAssembler.ToResourceFromEntity(updatedBeing);
                return Ok(resource);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Forbid(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while updating the being: {ex.Message}");
            }
        }

        [HttpDelete("{beingId:guid}")]
        public async Task<IActionResult> DeleteBeing([FromRoute] Guid beingId, [FromQuery] Guid userId)
        {
            try
            {
                var userCompanyId = await _userCompanyQueryService.FindUserCompanyIdByUserId(userId);
                if (!userCompanyId.HasValue)
                {
                    return Forbid("User is not associated with any company.");
                }

                var being = await _beingQueryService.Handle(new GetBeingByIdQuery(beingId));
                if (being is null) return NotFound();

                var userPermission = await _userCompanyPermissionRepository
                    .FindByUserCompanyAndPermissionTypeInModuleAsync(userCompanyId.Value, being.BeingModules.First().ModuleId, AllowedActionEnum.DELETE_BEING);

                if (userPermission == null)
                    return Forbid("User does not have permission to delete beings in this module.");

                var deleteBeingCommand = new DeleteBeingCommand(beingId, userCompanyId.Value);
                await _beingCommandService.Handle(deleteBeingCommand);
                return NoContent();
            }
            catch (UnauthorizedAccessException ex)
            {
                return Forbid(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while deleting the being: {ex.Message}");
            }
        }
    }
}