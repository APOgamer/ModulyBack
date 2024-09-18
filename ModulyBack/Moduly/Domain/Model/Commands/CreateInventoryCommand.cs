namespace ModulyBack.Moduly.Domain.Model.Commands
{
    public record CreateInventoryCommand(Guid UserId, Guid ModuleId, string Name, string Description);

}