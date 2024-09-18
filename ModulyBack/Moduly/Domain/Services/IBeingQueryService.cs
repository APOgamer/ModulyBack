using ModulyBack.Moduly.Domain.Model.Aggregate;
using ModulyBack.Moduly.Domain.Model.Entities;
using ModulyBack.Moduly.Domain.Model.Queries;

namespace ModulyBack.Moduly.Domain.Services;

public interface IBeingQueryService
{
    Task<Being?> Handle(GetBeingByIdQuery query);
    Task<IEnumerable<Being>> Handle(GetBeingsByModuleQuery query);
    Task<BeingModule?> FindBeingModuleAsync(Guid beingId, Guid moduleId);
    Task<BeingModule?> Handle(CheckBeingModuleQuery query); // Add this line

}