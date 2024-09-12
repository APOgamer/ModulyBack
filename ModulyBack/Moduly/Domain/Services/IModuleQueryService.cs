using ModulyBack.Moduly.Domain.Model.Entities;
using ModulyBack.Moduly.Domain.Model.Queries;

namespace ModulyBack.Moduly.Domain.Services;

public interface IModuleQueryService
{
    Task<Module> Handle(GetModuleByIdQuery query);
    Task<IEnumerable<Module>> Handle(GetAllModulesQuery query);
    Task<IEnumerable<Module>> Handle(GetModulesByCompanyIdQuery query);
}