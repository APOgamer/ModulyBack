using System;

namespace ModulyBack.Moduly.Domain.Model.Commands
{
    public record UpdateInventoryCommand(Guid InventoryId, Guid UserId, string Name, string Description);

}