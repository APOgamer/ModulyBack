namespace ModulyBack.Moduly.Domain.Model.Commands;

public record CreateModuleCommand(
    string ModuleName,
    string ModuleType,
    Guid CompanyId
);