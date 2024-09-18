using ModulyBack.Moduly.Domain.Model.Commands;
using ModulyBack.Moduly.Domain.Model.Entities;
using ModulyBack.Moduly.Domain.Model.ValueObjects;
using ModulyBack.Moduly.Domain.Repositories;
using ModulyBack.Moduly.Domain.Services;
using ModulyBack.Shared.Domain.Repositories;

namespace ModulyBack.Moduly.Application.Internal.CommandServices;

public class ModuleCommandService : IModuleCommandService
{
    private readonly IModuleRepository _moduleRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICompanyRepository _companyRepository;
    private readonly IUserCompanyRepository _userCompanyRepository;
    private readonly IUserCompanyPermissionRepository _userCompanyPermissionRepository;
    private readonly IPermissionTypeRepository _permissionTypeRepository;

    public ModuleCommandService(
        IModuleRepository moduleRepository,
        IUnitOfWork unitOfWork,
        ICompanyRepository companyRepository,
        IUserCompanyRepository userCompanyRepository,
        IUserCompanyPermissionRepository userCompanyPermissionRepository,
        IPermissionTypeRepository permissionTypeRepository)
    {
        _moduleRepository = moduleRepository;
        _unitOfWork = unitOfWork;
        _companyRepository = companyRepository;
        _userCompanyRepository = userCompanyRepository;
        _userCompanyPermissionRepository = userCompanyPermissionRepository;
        _permissionTypeRepository = permissionTypeRepository;
    }

    public async Task Handle(CreateModuleCommand command)
    {
        if (!await HasPermission(command.UserId, command.CompanyId, AllowedActionEnum.CREATE_MODULE))
        {
            throw new UnauthorizedAccessException("User does not have permission to create modules.");
        }
        var module = new Module
        {
            ModuleName = command.ModuleName,
            ModuleType = command.ModuleType,
            CompanyId = command.CompanyId,
            CreationDate = DateTime.UtcNow
        };

        await _moduleRepository.AddAsync(module);
        await _unitOfWork.CompleteAsync();
    }

    public async Task Handle(UpdateModuleCommand command)
    {
        var module = await _moduleRepository.FindByIdAsync(command.Id);
        if (module == null)
            throw new Exception("Module not found");

        if (!await HasPermission(command.UserId, module.CompanyId, AllowedActionEnum.EDIT_MODULE))
        {
            throw new UnauthorizedAccessException("User does not have permission to update modules.");
        }
        module.ModuleName = command.ModuleName;
        module.ModuleType = command.ModuleType;

        await _moduleRepository.UpdateAsync(module);
        await _unitOfWork.CompleteAsync();
    }

    public async Task Handle(DeleteModuleCommand command)
    {
        var module = await _moduleRepository.FindByIdAsync(command.Id);
        if (module == null)
            throw new Exception("Module not found");
        
        if (!await HasPermission(command.UserId, module.CompanyId, AllowedActionEnum.DELETE_MODULE))
        {
            throw new UnauthorizedAccessException("User does not have permission to delete modules.");
        }
        await _moduleRepository.RemoveAsync(module);
        await _unitOfWork.CompleteAsync();
    }
    private async Task<bool> HasPermission(Guid userId, Guid companyId, AllowedActionEnum requiredAction)
    {
        var company = await _companyRepository.GetByIdAsync(companyId);
        if (company.CreatedById == userId)
        {
            return true; // Company creator has all permissions
        }

        var defaultModule = await _moduleRepository.GetFirstModuleByCompanyIdAsync(companyId);
        var userCompany = await _userCompanyRepository.FindByUserAndCompanyAsync(userId, companyId);
            
        if (userCompany == null)
        {
            return false;
        }

        var permissions = await _userCompanyPermissionRepository.GetByUserCompanyIdAndModuleIdAsync(userCompany.Id, defaultModule.Id);

        foreach (var permission in permissions)
        {
            var permissionType = await _permissionTypeRepository.GetByIdAsync(permission.PermissionTypeId);
            if (permissionType != null && permissionType.PermissionTypeActions
                    .Any(pta => pta.AllowedAction == requiredAction))
            {
                return true;
            }
        }

        return false;
    }
}