using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using ModulyBack.Moduly.Domain.Model.Commands;
using ModulyBack.Moduly.Domain.Model.Queries;
using ModulyBack.Moduly.Domain.Model.ValueObjects;
using ModulyBack.Moduly.Domain.Services;
using ModulyBack.Moduly.Interfaces.REST.Transform;
using ModulyBack.Moduly.Interfaces.REST.Resources;

namespace ModulyBack.Moduly.Interfaces;

[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
public class CompanyController : ControllerBase
{
    private readonly ICompanyCommandService _companyCommandService;
    private readonly ICompanyQueryService _companyQueryService;
    private readonly IInvitationCommandService _invitationCommandService;
    private readonly IInvitationQueryService _invitationQueryService;

    public CompanyController(
        ICompanyCommandService companyCommandService, 
        ICompanyQueryService companyQueryService,
        IInvitationCommandService invitationCommandService,
        IInvitationQueryService invitationQueryService)
    {
        _companyCommandService = companyCommandService;
        _companyQueryService = companyQueryService;
        _invitationCommandService = invitationCommandService;
        _invitationQueryService = invitationQueryService;
    }

    [HttpGet]   
    public async Task<IActionResult> GetAllCompanies()
    {
        var query = new GetAllCompaniesQuery();
        var companies = await _companyQueryService.Handle(query);
        var companyResources = companies.Select(CompanyResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(companyResources);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCompanyById(Guid id)
    {
        var query = new GetCompanyByIdQuery(id);
        var company = await _companyQueryService.Handle(query);
        if (company == null)
            return NotFound();
        var companyResource = CompanyResourceFromEntityAssembler.ToResourceFromEntity(company);
        return Ok(companyResource);
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateCompany([FromBody] CreateCompanyCommand command)
    {
        if (command == null)
        {
            return BadRequest("Invalid data.");
        }

        try
        {
            var createdCompanyId = await _companyCommandService.Handle(command);

            // El ID del nuevo recurso creado se pasa a la acción GetCompanyById
            return CreatedAtAction(nameof(GetCompanyById), new { id = createdCompanyId }, command);
        }
        catch (Exception ex)
        {
            // Log the exception
            return StatusCode(500, $"An error occurred while creating the company: {ex.Message}");
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCompany(Guid id, [FromBody] UpdateCompanyCommand command)
    {
        if (id != command.Id)
            return BadRequest("ID mismatch");
        
        try
        {
            var updatedCompany = await _companyCommandService.Handle(command);
            return Ok(updatedCompany);
        }
        catch (Exception ex) when (ex.Message == "Company not found")
        {
            return NotFound($"Company with ID {id} not found.");
        }
        catch (Exception ex) when (ex.Message == "User not associated with this company." || 
                                   ex.Message == "User does not have admin rights for this company.")
        {
            return Forbid(ex.Message);
        }
        catch (Exception ex)
        {
            // Log the exception
            return StatusCode(500, $"An error occurred while updating the company: {ex.Message}");
        }
    }
    [HttpGet("getbycreatorid/{creatorId}")]
    public async Task<IActionResult> GetCompaniesByCreatorId(Guid creatorId)
    {
        var query = new GetCompaniesByCreatorIdQuery(creatorId);
        var companies = await _companyQueryService.Handle(query);

        if (companies == null || !companies.Any())
            return NotFound();

        var companyResources = companies.Select(CompanyResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(companyResources);
    }
    [HttpGet("getemployeesbycompany/{companyId}")]
    public async Task<IActionResult> GetEmployeesByCompanyId(Guid companyId)
    {
        var query = new GetEmployeesByCompanyIdQuery(companyId);
        var userCompanies = await _companyQueryService.Handle(query);

        if (userCompanies == null || !userCompanies.Any())
            return NotFound();

        var employeeResources = userCompanies.Select(UserCompanyResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(employeeResources);
    }

    [HttpPost("{companyId}/banks")]
    public async Task<IActionResult> CreateBank(Guid companyId, [FromBody] CreateBankResource resource)
    {
        if (resource == null)
        {
            return BadRequest("Invalid data.");
        }

        try
        {
            var command = CreateBankCommandFromResourceAssembler.ToCommandFromResource(companyId, resource);
            var createdBank = await _companyCommandService.CreateBank(command);

            var bankResource = BankResourceFromEntityAssembler.ToResourceFromEntity(createdBank);

            return CreatedAtAction(nameof(GetCompanyById), new { id = companyId }, bankResource);
        }
        catch (Exception ex) when (ex.Message == "Company not found")
        {
            return NotFound($"Company with ID {companyId} not found.");
        }
        catch (Exception ex)
        {
            // Log the exception
            return StatusCode(500, $"An error occurred while creating the bank: {ex.Message}");
        }
    }
    [HttpPost("invitations")]
public async Task<IActionResult> CreateInvitation([FromBody] CreateInvitationResource createInvitationResource)
{
    try
    {
        var createInvitationCommand = CreateInvitationCommandFromResourceAssembler.ToCommandFromResource(createInvitationResource);

        var company = await _companyQueryService.Handle(new GetCompanyByIdQuery(createInvitationCommand.CompanyId));
        if (company == null)
        {
            return NotFound($"Company with ID {createInvitationCommand.CompanyId} not found.");
        }

        if (company.CreatedById != createInvitationCommand.TransmitterId)
        {
            var hasPermission = await _companyQueryService.Handle(new CheckUserPermissionQuery(
                createInvitationCommand.TransmitterId, 
                createInvitationCommand.CompanyId, 
                AllowedActionEnum.SEND_INVITATIONS));

            if (!hasPermission)
            {
                return Forbid("User does not have permission to send invitations for this company.");
            }
        }

        var createdInvitation = await _invitationCommandService.Handle(createInvitationCommand);
        var invitationResource = InvitationResourceFromEntityAssembler.ToResourceFromEntity(createdInvitation);
        
        return CreatedAtAction(nameof(GetInvitationById), new { id = invitationResource.Id }, invitationResource);
    }
    catch (InvalidOperationException ex)
    {
        return BadRequest(ex.Message);
    }
    catch (Exception ex)
    {
        return StatusCode(500, "An internal server error occurred while creating the invitation");
    }
}
    [HttpPut("invitations/{id}/status")]
    public async Task<IActionResult> UpdateInvitationStatus(Guid id, [FromBody] UpdateInvitationStatusResource updateStatusResource)
    {
        var updateStatusCommand = new UpdateInvitationStatusCommand(id, updateStatusResource.Status);

        try
        {
            var updatedInvitation = await _invitationCommandService.Handle(updateStatusCommand);
            
            if (updatedInvitation == null)
            {
                return NotFound($"Invitation with ID {id} not found.");
            }

            var invitationResource = InvitationResourceFromEntityAssembler.ToResourceFromEntity(updatedInvitation);
            return Ok(invitationResource);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            // Log the exception
            return StatusCode(500, $"An error occurred while updating the invitation status: {ex.Message}");
        }
    }

    [HttpGet("invitations/{id}")]
    public async Task<IActionResult> GetInvitationById(Guid id)
    {
        var query = new GetInvitationByIdQuery(id);
        var invitation = await _invitationQueryService.Handle(query);

        if (invitation == null)
            return NotFound();

        var invitationResource = InvitationResourceFromEntityAssembler.ToResourceFromEntity(invitation);
        return Ok(invitationResource);
    }
    [HttpGet("invitations/pending/{userId}")]
    public async Task<IActionResult> GetPendingInvitations(Guid userId)
    {
        try
        {
            var query = new GetPendingInvitationsQuery(userId);
            var pendingInvitations = await _invitationQueryService.Handle(query);

            if (!pendingInvitations.Any())
                return NotFound();

            var invitationResources = pendingInvitations.Select(InvitationResourceFromEntityAssembler.ToResourceFromEntity);
            return Ok(invitationResources);
        }
        catch (InvalidOperationException ex)
        {
            return StatusCode(500, $"An error occurred while processing your request: {ex.Message}");
        }
    }

    [HttpGet("invitations/sent/{transmitterId}")]
    public async Task<IActionResult> GetSentInvitations(Guid transmitterId)
    {
        try
        {
            var query = new GetSentInvitationsQuery(transmitterId);
            var sentInvitations = await _invitationQueryService.Handle(query);

            if (!sentInvitations.Any())
                return NotFound();

            var invitationResources = sentInvitations.Select(InvitationResourceFromEntityAssembler.ToResourceFromEntity);
            return Ok(invitationResources);
        }
        catch (InvalidOperationException ex)
        {
            return StatusCode(500, $"An error occurred while processing your request: {ex.Message}");
        }
    }
}