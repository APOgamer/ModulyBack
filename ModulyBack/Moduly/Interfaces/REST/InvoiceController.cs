using ModulyBack.Moduly.Domain.Model.Commands;
using ModulyBack.Moduly.Domain.Model.Queries;
using ModulyBack.Moduly.Domain.Model.ValueObjects;
using ModulyBack.Moduly.Domain.Repositories;
using ModulyBack.Moduly.Domain.Services;
using ModulyBack.Moduly.Interfaces.REST.Resources;
using ModulyBack.Moduly.Interfaces.REST.Transform;

namespace ModulyBack.Moduly.Interfaces.REST;

using Microsoft.AspNetCore.Mvc;

using ModulyBack.Moduly.Interfaces.REST.Resources;
using ModulyBack.Moduly.Interfaces.REST.Transform;

using ModulyBack.Moduly.Domain.Model.Commands;
using ModulyBack.Moduly.Domain.Model.Queries;
using ModulyBack.Moduly.Domain.Services;



[ApiController]
[Route("api/v1/[controller]")]
public class InvoiceController : ControllerBase
    {
        private readonly IInvoiceCommandService _invoiceCommandService;
        private readonly IInvoiceQueryService _invoiceQueryService;
        private readonly IUserCompanyQueryService _userCompanyQueryService;
        private readonly ICompanyQueryService _companyQueryService;
        private readonly IUserCompanyPermissionRepository _userCompanyPermissionRepository;

        public InvoiceController(
            IInvoiceCommandService invoiceCommandService, 
            IInvoiceQueryService invoiceQueryService,
            IUserCompanyQueryService userCompanyQueryService,
            ICompanyQueryService companyQueryService,
            IUserCompanyPermissionRepository userCompanyPermissionRepository)
        {
            _invoiceCommandService = invoiceCommandService;
            _invoiceQueryService = invoiceQueryService;
            _userCompanyQueryService = userCompanyQueryService;
            _companyQueryService = companyQueryService;
            _userCompanyPermissionRepository = userCompanyPermissionRepository;
        }

 [HttpPost]
        public async Task<IActionResult> CreateInvoice([FromBody] CreateInvoiceResource createInvoiceResource)
        {
            var createInvoiceCommand = CreateInvoiceCommandFromResourceAssembler.ToCommandFromResource(createInvoiceResource);

            try
            {
                // First, verify if the user is the creator of the company
                var company = await _companyQueryService.Handle(new GetCompanyByModuleIdQuery(createInvoiceCommand.ModuleId));
                if (company != null && company.CreatedById == createInvoiceCommand.UserId)
                {
                    // The user is the creator, has automatic permission
                    var creatorInvoice = await _invoiceCommandService.Handle(createInvoiceCommand);
                    var creatorResource = InvoiceResourceFromEntityAssembler.ToResourceFromEntity(creatorInvoice);
                    return CreatedAtAction(nameof(GetInvoiceById), new { invoiceId = creatorResource.Id }, creatorResource);
                }

                // If not the creator, then check for UserCompanyId
                var userCompanyId = await _userCompanyQueryService.FindUserCompanyIdByUserId(createInvoiceCommand.UserId);
                if (!userCompanyId.HasValue)
                {
                    return Forbid("User is not associated with any company.");
                }

                // If not the creator, permission verification will be done in the InvoiceCommandService
                var regularInvoice = await _invoiceCommandService.Handle(createInvoiceCommand);

                if (regularInvoice is null) return BadRequest();

                var regularResource = InvoiceResourceFromEntityAssembler.ToResourceFromEntity(regularInvoice);
                return CreatedAtAction(nameof(GetInvoiceById), new { invoiceId = regularResource.Id }, regularResource);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Forbid(ex.Message);
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, $"An error occurred while creating the invoice: {ex.Message}");
            }
        }

    [HttpGet("{invoiceId:guid}")]
    public async Task<IActionResult> GetInvoiceById([FromRoute] Guid invoiceId)
    {
        var invoice = await _invoiceQueryService.Handle(new GetInvoiceByIdQuery(invoiceId));
        if (invoice is null) return NotFound();
        var resource = InvoiceResourceFromEntityAssembler.ToResourceFromEntity(invoice);
        return Ok(resource);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllInvoices()
    {
        var getAllInvoicesQuery = new GetAllInvoicesQuery();
        var invoices = await _invoiceQueryService.Handle(getAllInvoicesQuery);
        var resources = invoices.Select(InvoiceResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resources);
    }

    [HttpGet("byIssuer/{issuerId:guid}")]
    public async Task<IActionResult> GetInvoicesByIssuerId([FromRoute] Guid issuerId)
    {
        var query = new GetInvoicesByIssuerIdQuery(issuerId);
        var invoices = await _invoiceQueryService.Handle(query);
        var resources = invoices.Select(InvoiceResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resources);
    }

    [HttpPut("{invoiceId:guid}")]
    public async Task<IActionResult> UpdateInvoice([FromRoute] Guid invoiceId, [FromBody] UpdateInvoiceResource updateInvoiceResource)
    {
        // Obtener la factura actual para obtener los valores adicionales necesarios
        var invoice = await _invoiceQueryService.Handle(new GetInvoiceByIdQuery(invoiceId));
        if (invoice is null) return NotFound();

        // Llamar al ensamblador con los valores necesarios
        var updateInvoiceCommand = UpdateInvoiceCommandFromResourceAssembler.ToCommandFromResource(
            invoiceId,
            updateInvoiceResource,
            invoice.ModuleId,
            invoice.IssuerId,
            invoice.IssueDate
        );

        // Ejecutar el comando de actualización
        await _invoiceCommandService.Handle(updateInvoiceCommand);

        // Opcional: Obtener la factura actualizada para devolver la respuesta
        var updatedInvoice = await _invoiceQueryService.Handle(new GetInvoiceByIdQuery(invoiceId));
        if (updatedInvoice is null) return NotFound();

        var resource = InvoiceResourceFromEntityAssembler.ToResourceFromEntity(updatedInvoice);
        return Ok(resource);
    }



    [HttpDelete("{invoiceId:guid}")]
    public async Task<IActionResult> DeleteInvoice([FromRoute] Guid invoiceId, Guid userId)
    {
        var findUserCompanyIdByUserIdquery = new FindUserCompanyIdByUserIdquery(userId);
        var userCompanyId = await _userCompanyQueryService.Handle(findUserCompanyIdByUserIdquery);

        if (userCompanyId == Guid.Empty) // o si has usado Guid? y es nulo
        {
            throw new Exception($"No UserCompany found for userId {userId}");
        }

        var deleteInvoiceCommand = new DeleteInvoiceCommand(invoiceId, userCompanyId);
        await _invoiceCommandService.Handle(deleteInvoiceCommand); 
        return NoContent();
    }

    [HttpPut("{invoiceId:guid}/status")]
    public async Task<IActionResult> UpdateInvoiceStatus([FromRoute] Guid invoiceId, [FromBody] UpdateInvoiceStatusResource updateStatusResource, Guid userId)
    {
        var findUserCompanyIdByUserIdquery = new FindUserCompanyIdByUserIdquery(userId);
    
        // Usa 'await' para esperar el resultado de la tarea
        var userCompanyId = await _userCompanyQueryService.Handle(findUserCompanyIdByUserIdquery);
    
        if (userCompanyId == Guid.Empty)
        {
            return NotFound("UserCompany not found");
        }
    
        var updateStatusCommand = new UpdateInvoiceStatusCommand(invoiceId, updateStatusResource.Status, userCompanyId);
        var invoice = await _invoiceCommandService.Handle(updateStatusCommand);
    
        if (invoice is null) return NotFound();
    
        var resource = InvoiceResourceFromEntityAssembler.ToResourceFromEntity(invoice);
        return Ok(resource);
    }

    [HttpGet("byModule/{moduleId:guid}")]
    public async Task<IActionResult> GetInvoicesByModule([FromRoute] Guid moduleId, [FromQuery] Guid userId)
    {
        try
        {
            var company = await _companyQueryService.Handle(new GetCompanyByModuleIdQuery(moduleId));
            if (company == null)
            {
                return NotFound("Company not found for this module.");
            }

            if (company.CreatedById == userId)
            {
                // User is the creator, skip permission check
                var creatorInvoices = await _invoiceQueryService.Handle(new GetInvoicesByModuleQuery(moduleId));
                var creatorResources = creatorInvoices.Select(InvoiceResourceFromEntityAssembler.ToResourceFromEntity);
                return Ok(creatorResources);
            }

            var userCompanyId = await _userCompanyQueryService.FindUserCompanyIdByUserId(userId);
            if (!userCompanyId.HasValue)
            {
                return Forbid("User is not associated with any company.");
            }

            var userPermission = await _userCompanyPermissionRepository
                .FindByUserCompanyAndPermissionTypeInModuleAsync(userCompanyId.Value, moduleId, AllowedActionEnum.VIEW_INVOICE);

            if (userPermission == null)
                return Forbid($"User does not have permission to view invoices in this module.");

            var query = new GetInvoicesByModuleQuery(moduleId);
            var regularInvoices = await _invoiceQueryService.Handle(query);
            var regularResources = regularInvoices.Select(InvoiceResourceFromEntityAssembler.ToResourceFromEntity);
            return Ok(regularResources);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while retrieving invoices: {ex.Message}");
        }
    }
}