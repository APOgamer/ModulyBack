using ModulyBack.Moduly.Domain.Model.Commands;
using ModulyBack.Moduly.Domain.Model.Entities;

namespace ModulyBack.Moduly.Domain.Services;

public interface IInventoryCommandService
{
    Task<Inventory> Handle(CreateInventoryCommand command);
    Task<Inventory> Handle(UpdateInventoryCommand command);
    Task Handle(DeleteInventoryCommand command);
    Task<Inventory> Handle(AddStockCommand command);
    Task<Inventory> Handle(RemoveStockCommand command);
}