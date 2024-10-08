﻿using ModulyBack.Moduly.Domain.Model.Entities;
using ModulyBack.Shared.Domain.Repositories;

namespace ModulyBack.Moduly.Domain.Repositories;

public interface IUserCompanyRepository : IBaseRepository<UserCompany>
{
    Task<UserCompany?> FindByUserAndCompanyAsync(Guid userId, Guid companyId);
    Task<Guid?> FindUserCompanyIdByUserId(Guid userId);
    Task<IEnumerable<UserCompany>> FindByCompanyIdAsync(Guid companyId);
}