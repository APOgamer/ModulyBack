using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using ModulyBack.Moduly.Domain.Model.Commands;
using ModulyBack.Moduly.Domain.Model.Queries;
using ModulyBack.Moduly.Domain.Services;
using ModulyBack.Moduly.Interfaces.REST.Transform;

namespace ModulyBack.Moduly.Interfaces;

[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
public class CompanyController : ControllerBase
{
    private readonly ICompanyCommandService _companyCommandService;
    private readonly ICompanyQueryService _companyQueryService;

    public CompanyController(ICompanyCommandService companyCommandService, ICompanyQueryService companyQueryService)
    {
        _companyCommandService = companyCommandService;
        _companyQueryService = companyQueryService;
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
        await _companyCommandService.Handle(command);
        return CreatedAtAction(nameof(GetCompanyById), null);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCompany(Guid id, [FromBody] UpdateCompanyCommand command)
    {
        if (id != command.Id)
            return BadRequest("ID mismatch");
        
        await _companyCommandService.Handle(command);
        return NoContent();
    }
}