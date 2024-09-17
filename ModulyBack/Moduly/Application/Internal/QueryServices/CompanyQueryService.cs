using ModulyBack.Moduly.Domain.Model.Entities;
using ModulyBack.Moduly.Domain.Model.Queries;
using ModulyBack.Moduly.Domain.Repositories;
using ModulyBack.Moduly.Domain.Services;

namespace ModulyBack.Moduly.Application.Internal.QueryServices;

public class CompanyQueryService : ICompanyQueryService
{
    private readonly ICompanyRepository _companyRepository;

    public CompanyQueryService(ICompanyRepository companyRepository)
    {
        _companyRepository = companyRepository;
    }

    public async Task<Company> Handle(GetCompanyByIdQuery query)
    {
        return await _companyRepository.FindByIdAsync(query.Id);
    }

    public async Task<IEnumerable<Company>> Handle(GetAllCompaniesQuery query)
    {
        return await _companyRepository.GetAllAsync();
    }
    public async Task<Company?> Handle(GetCompanyByModuleIdQuery query)
    {
        return await _companyRepository.GetCompanyByModuleIdAsync(query.ModuleId);
    }
    public async Task<Company?> GetCompanyByIdAsync(Guid companyId)
    {
        return await _companyRepository.FindByIdAsync(companyId);
    }
}