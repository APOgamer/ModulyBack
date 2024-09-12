using ModulyBack.Moduly.Domain.Model.Commands;
using ModulyBack.Moduly.Domain.Model.Entities;
using ModulyBack.Moduly.Domain.Repositories;
using ModulyBack.Moduly.Domain.Services;
using ModulyBack.Shared.Domain.Repositories;

namespace ModulyBack.Moduly.Application.Internal.CommandServices;

public class ModuleCommandService : IModuleCommandService
{
    private readonly IModuleRepository _moduleRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ModuleCommandService(IModuleRepository moduleRepository, IUnitOfWork unitOfWork)
    {
        _moduleRepository = moduleRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(CreateModuleCommand command)
    {
        var module = new Module
        {
            ModuleName = command.ModuleName,
            ModuleType = command.ModuleType,
            CompanyId = command.CompanyId,
            CreationDate = DateTime.UtcNow
        };

        await _moduleRepository.AddAsync(module);
        await _unitOfWork.CompleteAsync();
    }

    public async Task Handle(UpdateModuleCommand command)
    {
        var module = await _moduleRepository.FindByIdAsync(command.Id);
        if (module == null)
            throw new Exception("Module not found");

        module.ModuleName = command.ModuleName;
        module.ModuleType = command.ModuleType;

        await _moduleRepository.UpdateAsync(module);
        await _unitOfWork.CompleteAsync();
    }

    public async Task Handle(DeleteModuleCommand command)
    {
        var module = await _moduleRepository.FindByIdAsync(command.Id);
        if (module == null)
            throw new Exception("Module not found");

        await _moduleRepository.RemoveAsync(module);
        await _unitOfWork.CompleteAsync();
    }
}