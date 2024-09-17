﻿using Microsoft.AspNetCore.Mvc;
using ModulyBack.Moduly.Domain.Model.Commands;
using ModulyBack.Moduly.Domain.Services;
using ModulyBack.Moduly.Interfaces.REST.Resources;
using ModulyBack.Moduly.Interfaces.REST.Transform;

namespace ModulyBack.Moduly.Interfaces.REST;

[ApiController]
[Route("api/v1/[controller]")]
public class PermissionAssignmentController : ControllerBase
{
    private readonly IPermissionAssignmentCommandService _permissionAssignmentCommandService;
    private readonly ICompanyQueryService _companyQueryService;
    private readonly IUserCompanyQueryService _userCompanyQueryService;
    private readonly IPermissionTypeCommandService _permissionTypeCommandService;

    public PermissionAssignmentController(
        IPermissionAssignmentCommandService permissionAssignmentCommandService,
        IPermissionTypeCommandService permissionTypeCommandService,
        ICompanyQueryService companyQueryService,
        IUserCompanyQueryService userCompanyQueryService)
    {
        _permissionAssignmentCommandService = permissionAssignmentCommandService;
        _permissionTypeCommandService = permissionTypeCommandService;
        _companyQueryService = companyQueryService;
        _userCompanyQueryService = userCompanyQueryService;
    }

    [HttpPost("assign")]
    public async Task<IActionResult> AssignPermission([FromBody] AssignPermissionResource assignPermissionResource)
    {
        var command = AssignPermissionCommandFromResourceAssembler.ToCommandFromResource(assignPermissionResource);

        try
        {
            // Verificar si el usuario asignador es el creador de la compañía o tiene permisos para asignar
            var company = await _companyQueryService.GetCompanyByIdAsync(command.CompanyId);

            if (company == null)
            {
                return NotFound("Company not found.");
            }

            if (company.CreatedById != command.AssignerUserId)
            {
                // Verificar si el asignador tiene permiso para asignar permisos
                var assignerUserCompanyId = await _userCompanyQueryService.FindUserCompanyIdByUserId(command.AssignerUserId);
                if (!assignerUserCompanyId.HasValue)
                {
                    return Forbid("Assigner is not associated with the company.");
                }

                var hasPermissionToAssign = await _permissionAssignmentCommandService.UserHasPermissionToAssign(assignerUserCompanyId.Value, command.CompanyId);
                if (!hasPermissionToAssign)
                {
                    return Forbid("User does not have permission to assign permissions.");
                }
            }

            // Proceder con la asignación de permisos
            var result = await _permissionAssignmentCommandService.Handle(command);
            return Ok(result);
        }
        catch (Exception ex)
        {
            // Log the exception
            return StatusCode(500, $"An error occurred while assigning permissions: {ex.Message}");
        }
    }
    [HttpPost("create-permission-type")]
    public async Task<IActionResult> CreatePermissionType([FromBody] CreatePermissionTypeResource resource)
    {
        var command = CreatePermissionTypeCommandFromResourceAssembler.ToCommand(resource);

        try
        {
            var company = await _companyQueryService.GetCompanyByIdAsync(command.CompanyId);
            if (company == null)
            {
                return NotFound("Company not found.");
            }

            // Verificar si el usuario es miembro de la compañía
            var userCompanyId = await _userCompanyQueryService.FindUserCompanyIdByUserId(resource.UserId);
            if (!userCompanyId.HasValue || userCompanyId.Value != command.CompanyId)
            {
                return Forbid("User is not associated with the company.");
            }

            // Crear el nuevo tipo de permiso
            var result = await _permissionTypeCommandService.Handle(command);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while creating permission type: {ex.Message}");
        }
    }
    [HttpGet("company/{companyId}/permission-types")]
    public async Task<IActionResult> GetPermissionTypesByCompany(Guid companyId)
    {
        try
        {
            var company = await _companyQueryService.GetCompanyByIdAsync(companyId);
            if (company == null)
            {
                return NotFound("Company not found.");
            }

            var permissionTypes = await _permissionTypeCommandService.GetPermissionTypesByCompanyId(companyId);
            var permissionTypeResources = PermissionTypeResourceAssembler.ToResourceList(permissionTypes);

            return Ok(permissionTypeResources);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while retrieving permission types: {ex.Message}");
        }
    }

}