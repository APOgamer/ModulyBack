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
}