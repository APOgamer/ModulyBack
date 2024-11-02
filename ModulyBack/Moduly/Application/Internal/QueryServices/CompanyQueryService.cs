using ModulyBack.Moduly.Domain.Model.Entities;
using ModulyBack.Moduly.Domain.Model.Queries;
using ModulyBack.Moduly.Domain.Repositories;
using ModulyBack.Moduly.Domain.Services;

namespace ModulyBack.Moduly.Application.Internal.QueryServices;

public class CompanyQueryService : ICompanyQueryService
{
    private readonly ICompanyRepository _companyRepository;
    private readonly IUserCompanyRepository _userCompanyRepository;
    private readonly IUserCompanyPermissionRepository _userCompanyPermissionRepository;

    public CompanyQueryService(
        ICompanyRepository companyRepository, 
        IUserCompanyRepository userCompanyRepository,
        IUserCompanyPermissionRepository userCompanyPermissionRepository)
    {
        _companyRepository = companyRepository;
        _userCompanyRepository = userCompanyRepository;
        _userCompanyPermissionRepository = userCompanyPermissionRepository;
    }
    public async Task<bool> Handle(CheckUserPermissionQuery query)
    {

        var userCompany = await _userCompanyRepository.FindByUserAndCompanyAsync(query.UserId, query.CompanyId);

        if (userCompany == null)
        {
            return false;
        }

        var userPermission = await _userCompanyPermissionRepository
            .FindByUserCompanyAndPermissionTypeAsync(userCompany.Id, query.PermissionType);

        return userPermission != null;
    }
    public async Task<Company> Handle(GetCompanyByIdQuery query)
    {
        return await _companyRepository.FindByIdAsync(query.Id);
    }
    public async Task<IEnumerable<UserCompany>> Handle(GetEmployeesByCompanyIdQuery query)
    {
        return await _userCompanyRepository.FindByCompanyIdAsync(query.CompanyId);
    }
    public async Task<IEnumerable<Company>> Handle(GetAllCompaniesQuery query)
    {
        return await _companyRepository.GetAllAsync();
    }
    public async Task<Company?> Handle(GetCompanyByModuleIdQuery query)
    {
        return await _companyRepository.GetCompanyByModuleIdAsync(query.ModuleId);
    }
    public async Task<IEnumerable<Company>> Handle(GetCompaniesByCreatorIdQuery query)
    {
        return await _companyRepository.FindByCreatorIdAsync(query.CreatorId);
    }

    public async Task<Company?> GetCompanyByIdAsync(Guid companyId)
    {
        return await _companyRepository.FindByIdAsync(companyId);
    }
}