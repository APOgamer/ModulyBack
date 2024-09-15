namespace ModulyBack.Moduly.Interfaces.REST;

using Microsoft.AspNetCore.Mvc;
using ModulyBack.Moduly.Domain.Model.Commands;
using ModulyBack.Moduly.Domain.Model.Queries;
using ModulyBack.Moduly.Domain.Services;
using ModulyBack.Moduly.Interfaces.REST.Resources;
using ModulyBack.Moduly.Interfaces.REST.Transform;


[ApiController]
[Route("api/v1/[controller]")]
public class InvoiceController : ControllerBase
{
    private readonly IInvoiceCommandService _invoiceCommandService;
    private readonly IInvoiceQueryService _invoiceQueryService;
    private readonly IUserCompanyQueryService _userCompanyQueryService;

    public InvoiceController(IInvoiceCommandService invoiceCommandService, IInvoiceQueryService invoiceQueryService)
    {
        _invoiceCommandService = invoiceCommandService;
        _invoiceQueryService = invoiceQueryService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateInvoice([FromBody] CreateInvoiceResource createInvoiceResource)
    {
        var createInvoiceCommand = CreateInvoiceCommandFromResourceAssembler.ToCommandFromResource(createInvoiceResource);

        // Ejecutar el comando de creación
        var invoice = await _invoiceCommandService.Handle(createInvoiceCommand);

        // Obtener la factura recién creada para devolver la respuesta
        if (invoice is null) return BadRequest();

        var resource = InvoiceResourceFromEntityAssembler.ToResourceFromEntity(invoice);
        return CreatedAtAction(nameof(GetInvoiceById), new { invoiceId = resource.Id }, resource);
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


}