using ModulyBack.Moduly.Domain.Model.Commands;
using ModulyBack.Moduly.Domain.Model.Entities;

namespace ModulyBack.Moduly.Domain.Services;


public interface ICompanyCommandService
{
    Task<Guid> Handle(CreateCompanyCommand command);
    Task<Company> Handle(UpdateCompanyCommand command);
    Task<Bank> CreateBank(CreateBankCommand command);
}