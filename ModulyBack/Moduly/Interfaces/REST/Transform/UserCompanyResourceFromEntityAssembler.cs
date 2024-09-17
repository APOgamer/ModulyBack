using ModulyBack.Moduly.Domain.Model.Entities;
using ModulyBack.Moduly.Interfaces.REST.Resources;

namespace ModulyBack.Moduly.Interfaces.REST.Transform;

public static class UserCompanyResourceFromEntityAssembler
{
    public static UserCompanyResource ToResourceFromEntity(UserCompany userCompany)
    {
        return new UserCompanyResource
        {
            Id = userCompany.Id,
            UserId = userCompany.UserId,
            CompanyId = userCompany.CompanyId,
            Role = userCompany.Role,
            JoinDate = userCompany.JoinDate
            // Mapear otros campos según sea necesario
        };
    }
}