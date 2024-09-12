using Microsoft.AspNetCore.Mvc;
using ModulyBack.Moduly.Domain.Model.Commands;
using ModulyBack.Moduly.Domain.Model.Queries;
using ModulyBack.Moduly.Domain.Services;
using ModulyBack.Moduly.Interfaces.Resources;
using ModulyBack.Moduly.Interfaces.REST.Resources;
using ModulyBack.Moduly.Interfaces.REST.Transform;

namespace ModulyBack.Moduly.Interfaces.REST;

 [ApiController]
    [Route("api/v1/[controller]")]
    public class ModuleController : ControllerBase
    {
        private readonly IModuleCommandService _moduleCommandService;
        private readonly IModuleQueryService _moduleQueryService;

        public ModuleController(IModuleCommandService moduleCommandService, IModuleQueryService moduleQueryService)
        {
            _moduleCommandService = moduleCommandService;
            _moduleQueryService = moduleQueryService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ModuleResource>>> GetAllModules()
        {
            var modules = await _moduleQueryService.Handle(new GetAllModulesQuery());
            var resources = modules.Select(ModuleResourceFromEntityAssembler.ToResourceFromEntity);
            return Ok(resources);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ModuleResource>> GetModuleById(Guid id)
        {
            var module = await _moduleQueryService.Handle(new GetModuleByIdQuery(id));
            if (module == null)
                return NotFound();

            var resource = ModuleResourceFromEntityAssembler.ToResourceFromEntity(module);
            return Ok(resource);
        }

        [HttpGet("company/{companyId}")]
        public async Task<ActionResult<IEnumerable<ModuleResource>>> GetModulesByCompanyId(Guid companyId)
        {
            var modules = await _moduleQueryService.Handle(new GetModulesByCompanyIdQuery(companyId));
            var resources = modules.Select(ModuleResourceFromEntityAssembler.ToResourceFromEntity);
            return Ok(resources);
        }

        [HttpPost]
        public async Task<ActionResult> CreateModule([FromBody] CreateModuleCommand command)
        {
            await _moduleCommandService.Handle(command);
            return CreatedAtAction(nameof(GetModuleById), new { id = command.CompanyId }, null);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateModule(Guid id, [FromBody] UpdateModuleCommand command)
        {
            if (id != command.Id)
                return BadRequest();

            await _moduleCommandService.Handle(command);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteModule(Guid id)
        {
            await _moduleCommandService.Handle(new DeleteModuleCommand(id));
            return NoContent();
        }
    }