using ModulyBack.Moduly.Domain.Model.Commands;
using ModulyBack.Moduly.Interfaces.REST.Resources;

namespace ModulyBack.Moduly.Interfaces.REST.Transform
{
    public static class UpdateInventoryCommandFromResourceAssembler
    {
        public static UpdateInventoryCommand ToCommandFromResource(Guid inventoryId, UpdateInventoryResource resource)
        {
            return new UpdateInventoryCommand(
                inventoryId,
                resource.UserId,
                resource.Name,
                resource.Description
            );
        }
    }
}