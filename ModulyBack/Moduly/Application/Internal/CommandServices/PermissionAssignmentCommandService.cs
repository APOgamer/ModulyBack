using ModulyBack.Moduly.Domain.Model.Aggregate;
using ModulyBack.Moduly.Domain.Model.Commands;
using ModulyBack.Moduly.Domain.Model.ValueObjects;
using ModulyBack.Moduly.Domain.Repositories;
using ModulyBack.Moduly.Domain.Services;
using ModulyBack.Shared.Domain.Repositories;

namespace ModulyBack.Moduly.Application.Internal.CommandServices;

public class PermissionAssignmentCommandService : IPermissionAssignmentCommandService
{
    private readonly IUserCompanyRepository _userCompanyRepository;
    private readonly IUserCompanyPermissionRepository _userCompanyPermissionRepository;
    private readonly IPermissionTypeRepository _permissionTypeRepository;
    private readonly IUnitOfWork _unitOfWork;

    public PermissionAssignmentCommandService(
        IUserCompanyRepository userCompanyRepository,
        IUserCompanyPermissionRepository userCompanyPermissionRepository,
        IPermissionTypeRepository permissionTypeRepository,
        IUnitOfWork unitOfWork)
    {
        _userCompanyRepository = userCompanyRepository;
        _userCompanyPermissionRepository = userCompanyPermissionRepository;
        _permissionTypeRepository = permissionTypeRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(AssignPermissionCommand command)
    {
        var userCompany = await _userCompanyRepository.FindByUserAndCompanyAsync(command.TargetUserId, command.CompanyId);
        if (userCompany == null)
        {
            throw new Exception("Target user is not associated with the company.");
        }

        var permissionType = await _permissionTypeRepository.FindByIdAsync(command.PermissionTypeId);
        if (permissionType == null)
        {
            throw new Exception("Permission type not found.");
        }

        var existingPermission = await _userCompanyPermissionRepository.FindByUserCompanyAndPermissionTypeInModuleAsync(
            userCompany.Id, command.ModuleId, command.AllowedAction);

        if (existingPermission != null)
        {
            existingPermission.IsGranted = true;
            await _userCompanyPermissionRepository.UpdateAsync(existingPermission);
        }
        else
        {
            var newPermission = new UserCompanyPermission
            {
                UserCompanyId = userCompany.Id,
                ModuleId = command.ModuleId,
                PermissionTypeId = permissionType.Id,
                IsGranted = true
            };
            await _userCompanyPermissionRepository.AddAsync(newPermission);
        }

        await _unitOfWork.CompleteAsync();
        return true;
    }

    public async Task<bool> UserHasPermissionToAssign(Guid userCompanyId, Guid companyId)
    {
        var adminPermission = await _userCompanyPermissionRepository.FindByUserCompanyAndPermissionTypeAsync(
            userCompanyId, AllowedActionEnum.ADMIN);

        var assignPerksPermission = await _userCompanyPermissionRepository.FindByUserCompanyAndPermissionTypeAsync(
            userCompanyId, AllowedActionEnum.ASSIGN_PERK);

        return (adminPermission != null && adminPermission.IsGranted) || 
               (assignPerksPermission != null && assignPerksPermission.IsGranted);
    }

}