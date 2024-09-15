using Microsoft.EntityFrameworkCore;
using ModulyBack.Moduly.Domain.Model.Entities;
using ModulyBack.Moduly.Domain.Repositories;
using ModulyBack.Shared.Infraestructure.Persistences.EFC.Configuration;
using ModulyBack.Shared.Infraestructure.Persistences.EFC.Repositories;

namespace ModulyBack.Moduly.Infraestructure.Persistence.EFC.Repositories;

public class UserCompanyRepository : BaseRepository<UserCompany>, IUserCompanyRepository
{
    public UserCompanyRepository(AppDbContext context) : base(context) { }


    public async Task AddAsync(UserCompany userCompany)
    {
        await Context.UserCompanies.AddAsync(userCompany);
    }
    public async Task<UserCompany?> FindByUserAndCompanyAsync(Guid userId, Guid companyId)
    {
        return await Context.UserCompanies
            .FirstOrDefaultAsync(uc => uc.UserId == userId && uc.CompanyId == companyId);
    }
    public async Task<Guid?> FindUserCompanyIdByUserId(Guid userId)
    {
        var userCompany = await Context.UserCompanies
            .FirstOrDefaultAsync(uc => uc.UserId == userId);
        return userCompany.Id;
    }

}