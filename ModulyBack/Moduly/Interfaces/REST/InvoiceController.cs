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

    public InvoiceController(IInvoiceCommandService invoiceCommandService, IInvoiceQueryService invoiceQueryService)
    {
        _invoiceCommandService = invoiceCommandService;
        _invoiceQueryService = invoiceQueryService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateInvoice([FromBody] CreateInvoiceResource createInvoiceResource)
    {
        var createInvoiceCommand = CreateInvoiceCommandFromResourceAssembler.ToCommandFromResource(createInvoiceResource);
        var invoice = await _invoiceCommandService.Handle(createInvoiceCommand);
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
        var updateInvoiceCommand = UpdateInvoiceCommandFromResourceAssembler.ToCommandFromResource(invoiceId, updateInvoiceResource);
        var invoice = await _invoiceCommandService.Handle(updateInvoiceCommand);
        if (invoice is null) return NotFound();
        var resource = InvoiceResourceFromEntityAssembler.ToResourceFromEntity(invoice);
        return Ok(resource);
    }

    [HttpDelete("{invoiceId:guid}")]
    public async Task<IActionResult> DeleteInvoice([FromRoute] Guid invoiceId)
    {
        var deleteInvoiceCommand = new DeleteInvoiceCommand(invoiceId);
        var result = await _invoiceCommandService.Handle(deleteInvoiceCommand);
        if (!result) return NotFound();
        return NoContent();
    }

    [HttpPut("{invoiceId:guid}/status")]
    public async Task<IActionResult> UpdateInvoiceStatus([FromRoute] Guid invoiceId, [FromBody] UpdateInvoiceStatusResource updateStatusResource)
    {
        var updateStatusCommand = new UpdateInvoiceStatusCommand(invoiceId, updateStatusResource.Status);
        var invoice = await _invoiceCommandService.Handle(updateStatusCommand);
        if (invoice is null) return NotFound();
        var resource = InvoiceResourceFromEntityAssembler.ToResourceFromEntity(invoice);
        return Ok(resource);
    }
}