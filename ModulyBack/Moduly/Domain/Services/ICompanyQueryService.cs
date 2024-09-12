using ModulyBack.Moduly.Domain.Model.Entities;
using ModulyBack.Moduly.Domain.Model.Queries;

namespace ModulyBack.Moduly.Domain.Services;

public interface ICompanyQueryService
{
    Task<Company> Handle(GetCompanyByIdQuery query);
    Task<IEnumerable<Company>> Handle(GetAllCompaniesQuery query);
}