using ModulyBack.Moduly.Domain.Model.Aggregate;
using ModulyBack.Moduly.Domain.Model.Entities;
using ModulyBack.Moduly.Domain.Model.Queries;
using ModulyBack.Moduly.Domain.Repositories;
using ModulyBack.Moduly.Domain.Services;

namespace ModulyBack.Moduly.Application.Internal.QueryServices
{
    public class BeingQueryService : IBeingQueryService
    {
        private readonly IBeingRepository _beingRepository;

        public BeingQueryService(IBeingRepository beingRepository)
        {
            _beingRepository = beingRepository;
        }

        public async Task<Being?> Handle(GetBeingByIdQuery query)
        {
            return await _beingRepository.FindByIdAsync(query.Id);
        }

        public async Task<IEnumerable<Being>> Handle(GetBeingsByModuleQuery query)
        {
            return await _beingRepository.FindByModuleIdAsync(query.ModuleId);
        }

        public async Task<BeingModule?> FindBeingModuleAsync(Guid beingId, Guid moduleId)
        {
            return await _beingRepository.FindBeingModuleAsync(beingId, moduleId);
        }
        public async Task<BeingModule?> Handle(CheckBeingModuleQuery query)
        {
            return await _beingRepository.FindBeingModuleAsync(query.BeingId, query.ModuleId);
        }

    }
}