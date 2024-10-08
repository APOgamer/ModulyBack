using ModulyBack.Moduly.Domain.Model.Entities;
using ModulyBack.Moduly.Interfaces.REST.Resources;

namespace ModulyBack.Moduly.Interfaces.REST.Transform;

public static class ModuleResourceFromEntityAssembler
{
    public static ModuleResource ToResourceFromEntity(Module entity)
    {
        return new ModuleResource
        {
            Id = entity.Id,
            ModuleName = entity.ModuleName,
            ModuleType = entity.ModuleType,
            CompanyId = entity.CompanyId,
            CreationDate = entity.CreationDate
        };
    }
}