﻿using ModulyBack.Moduly.Domain.Model.Queries;

namespace ModulyBack.Moduly.Domain.Services;

public interface IUserCompanyQueryService
{
    Task<Guid> Handle(FindUserCompanyIdByUserIdquery query);

}