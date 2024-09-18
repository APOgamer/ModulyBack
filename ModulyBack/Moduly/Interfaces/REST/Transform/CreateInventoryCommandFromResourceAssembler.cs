using ModulyBack.Moduly.Domain.Model.Commands;
using ModulyBack.Moduly.Interfaces.REST.Resources;

namespace ModulyBack.Moduly.Interfaces.REST.Transform
{
    public static class CreateInventoryCommandFromResourceAssembler
    {
        public static CreateInventoryCommand ToCommandFromResource(CreateInventoryResource resource)
        {
            return new CreateInventoryCommand(
                resource.UserId,
                resource.ModuleId,
                resource.Name,
                resource.Description
            );
        }
    }
}