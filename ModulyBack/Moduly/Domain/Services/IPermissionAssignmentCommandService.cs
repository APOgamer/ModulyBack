using ModulyBack.Moduly.Domain.Model.Commands;

namespace ModulyBack.Moduly.Domain.Services;

public interface IPermissionAssignmentCommandService
{
    Task<bool> Handle(AssignPermissionCommand command);
    Task<bool> UserHasPermissionToAssign(Guid userCompanyId, Guid companyId);

}