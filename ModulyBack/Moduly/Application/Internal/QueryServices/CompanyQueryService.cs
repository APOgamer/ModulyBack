using ModulyBack.Moduly.Domain.Model.Entities;
using ModulyBack.Moduly.Domain.Model.Queries;
using ModulyBack.Moduly.Domain.Repositories;
using ModulyBack.Moduly.Domain.Services;

namespace ModulyBack.Moduly.Application.Internal.QueryServices;

public class CompanyQueryService : ICompanyQueryService
{
    private readonly ICompanyRepository _companyRepository;
    private readonly IUserCompanyRepository _userCompanyRepository;

    public CompanyQueryService(ICompanyRepository companyRepository, IUserCompanyRepository userCompanyRepository
    )
    {
        _companyRepository = companyRepository;
        _userCompanyRepository = userCompanyRepository;

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