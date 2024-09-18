using ModulyBack.Moduly.Domain.Model.Entities;
using ModulyBack.Moduly.Domain.Model.Queries;

namespace ModulyBack.Moduly.Domain.Services;

public interface IBeingQueryService
{
    Task<Being?> Handle(GetBeingByIdQuery query);
    Task<IEnumerable<Being>> Handle(GetBeingsByModuleQuery query);
}