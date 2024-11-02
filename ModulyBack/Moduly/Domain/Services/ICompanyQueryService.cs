using ModulyBack.Moduly.Domain.Model.Entities;
using ModulyBack.Moduly.Domain.Model.Queries;

namespace ModulyBack.Moduly.Domain.Services;

public interface ICompanyQueryService
{
    Task<Company> Handle(GetCompanyByIdQuery query);
    Task<IEnumerable<Company>> Handle(GetAllCompaniesQuery query);
    Task<Company?> Handle(GetCompanyByModuleIdQuery query);  // Nuevo método
    Task<Company?> GetCompanyByIdAsync(Guid companyId);
    Task<IEnumerable<Company>> Handle(GetCompaniesByCreatorIdQuery query);
    Task<IEnumerable<UserCompany>> Handle(GetEmployeesByCompanyIdQuery query);
    Task<bool> Handle(CheckUserPermissionQuery query);


}