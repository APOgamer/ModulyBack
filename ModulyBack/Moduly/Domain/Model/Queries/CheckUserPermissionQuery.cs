using ModulyBack.Moduly.Domain.Model.ValueObjects;

namespace ModulyBack.Moduly.Domain.Model.Queries;

public record CheckUserPermissionQuery(Guid UserId, Guid CompanyId, AllowedActionEnum PermissionType);
