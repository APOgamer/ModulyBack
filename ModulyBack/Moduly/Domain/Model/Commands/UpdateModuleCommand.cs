namespace ModulyBack.Moduly.Domain.Model.Commands;

public record UpdateModuleCommand(
    Guid Id,
    string ModuleName,
    string ModuleType,
    Guid UserId
);