using ModulyBack.Moduly.Domain.Model.Entities;
using ModulyBack.Moduly.Domain.Model.Queries;
using ModulyBack.Moduly.Domain.Repositories;
using ModulyBack.Moduly.Domain.Services;

namespace ModulyBack.Moduly.Application.Internal.QueryServices;

public class ModuleQueryService : IModuleQueryService
{
    private readonly IModuleRepository _moduleRepository;

    public ModuleQueryService(IModuleRepository moduleRepository)
    {
        _moduleRepository = moduleRepository;
    }

    public async Task<Module> Handle(GetModuleByIdQuery query)
    {
        return await _moduleRepository.FindByIdAsync(query.Id);
    }

    public async Task<IEnumerable<Module>> Handle(GetAllModulesQuery query)
    {
        return await _moduleRepository.GetAllAsync();
    }

    public async Task<IEnumerable<Module>> Handle(GetModulesByCompanyIdQuery query)
    {
        return await _moduleRepository.GetByCompanyIdAsync(query.CompanyId);
    }
}