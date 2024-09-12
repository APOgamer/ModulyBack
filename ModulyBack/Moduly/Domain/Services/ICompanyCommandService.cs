using ModulyBack.Moduly.Domain.Model.Commands;
using ModulyBack.Moduly.Domain.Model.Entities;

namespace ModulyBack.Moduly.Domain.Services;


public interface ICompanyCommandService
{
    Task Handle(CreateCompanyCommand command);
    Task Handle(UpdateCompanyCommand command);
}