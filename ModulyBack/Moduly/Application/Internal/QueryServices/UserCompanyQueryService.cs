using ModulyBack.Moduly.Domain.Model.Queries;
using ModulyBack.Moduly.Domain.Repositories;
using ModulyBack.Moduly.Domain.Services;

namespace ModulyBack.Moduly.Application.Internal.QueryServices;


public class UserCompanyQueryService : IUserCompanyQueryService
{
    private readonly IUserCompanyRepository _UserCompanyRepository;

    public UserCompanyQueryService(IUserCompanyRepository userCompanyRepository)
    {
        _UserCompanyRepository = userCompanyRepository;
    }


    public async Task<Guid> Handle(FindUserCompanyIdByUserIdquery query)
    {
        var userCompanyId = await _UserCompanyRepository.FindUserCompanyIdByUserId(query.userId);

        return userCompanyId ?? Guid.Empty; // Retorna un Guid.Empty si es nulo FALTA MANEJAR
    }
    public async Task<Guid?> FindUserCompanyIdByUserId(Guid userId)
    {
        return await _UserCompanyRepository.FindUserCompanyIdByUserId(userId);
    }


}