using ModulyBack.Moduly.Domain.Model.Commands;
using ModulyBack.Moduly.Domain.Model.Entities;

namespace ModulyBack.Moduly.Domain.Services;

public interface IBeingCommandService
{
    Task<Being> Handle(CreateBeingCommand command);
    Task<Being> Handle(UpdateBeingCommand command);
    Task Handle(DeleteBeingCommand command);
}