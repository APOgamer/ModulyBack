using ModulyBack.Moduly.Domain.Model.Commands;

namespace ModulyBack.Moduly.Domain.Services;

public interface IModuleCommandService
{
    Task Handle(CreateModuleCommand command);
    Task Handle(UpdateModuleCommand command);
    Task Handle(DeleteModuleCommand command);
}