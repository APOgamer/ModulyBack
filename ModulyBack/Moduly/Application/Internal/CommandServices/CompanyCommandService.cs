using ModulyBack.Moduly.Domain.Model.Commands;
using ModulyBack.Moduly.Domain.Model.Entities;
using ModulyBack.Moduly.Domain.Repositories;
using ModulyBack.Shared.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ModulyBack.Moduly.Domain.Model.Aggregate;
using ModulyBack.Moduly.Domain.Model.ValueObjects;
using ModulyBack.Moduly.Domain.Services;

namespace ModulyBack.Moduly.Application.Internal.CommandServices
{
    public class CompanyCommandService : ICompanyCommandService
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserCompanyRepository _userCompanyRepository;
        private readonly IPermissionTypeRepository _permissionTypeRepository;
        private readonly IUserCompanyPermissionRepository _userCompanyPermissionRepository;
        private readonly IModuleRepository _moduleRepository;
        private readonly IBankRepository _bankRepository;

        public CompanyCommandService(
            ICompanyRepository companyRepository, 
            IUnitOfWork unitOfWork,
            IUserCompanyRepository userCompanyRepository,
            IPermissionTypeRepository permissionTypeRepository,
            IUserCompanyPermissionRepository userCompanyPermissionRepository,
            IModuleRepository moduleRepository,
            IBankRepository bankRepository)
        {
            _companyRepository = companyRepository;
            _unitOfWork = unitOfWork;
            _userCompanyRepository = userCompanyRepository;
            _permissionTypeRepository = permissionTypeRepository;
            _userCompanyPermissionRepository = userCompanyPermissionRepository;
            _moduleRepository = moduleRepository;
            _bankRepository = bankRepository;
        }

        public async Task<Guid> Handle(CreateCompanyCommand command)
        {
            // Crear la nueva compañía
            var company = new Company
            {
                Id = Guid.NewGuid(),
                CompanyName = command.CompanyName,
                LegalName = command.LegalName,
                Ruc = command.Ruc,
                Address = command.Address,
                Email = command.Email,
                Password = command.Password,
                CreatedById = command.CreatedById
            };

            await _companyRepository.AddAsync(company);

            // Crear el UserCompany para el creador con el rol de "CREATOR"
            var userCompany = new UserCompany
            {
                Id = Guid.NewGuid(),
                UserId = command.CreatedById,
                CompanyId = company.Id,
                Role = "CREATOR",
                JoinDate = DateTime.UtcNow
            };

            await _userCompanyRepository.AddAsync(userCompany);

            // Crear un módulo predeterminado para la compañía
            var defaultModule = new Module
            {
                Id = Guid.NewGuid(),
                ModuleName = "Admin Module",
                ModuleType = "Here, permissions are assigned to employees and invitations are sent.",
                CompanyId = company.Id,
                CreationDate = DateTime.UtcNow
            };
            await _moduleRepository.AddAsync(defaultModule);

            // Crear el PermissionType con la acción ADMIN
            var permissionType = new PermissionType
            {
                Id = Guid.NewGuid(),
                Name = "Admin Permission",
                CompanyId = company.Id,
                PermissionTypeActions = new List<PermissionTypeAction>
                {
                    new PermissionTypeAction
                    {
                        Id = Guid.NewGuid(),
                        AllowedAction = AllowedActionEnum.ADMIN
                    }
                }
            };

            await _permissionTypeRepository.AddAsync(permissionType);

            // Crear UserCompanyPermission para el creador con el PermissionType ADMIN
            var userCompanyPermission = new UserCompanyPermission
            {
                Id = Guid.NewGuid(),
                UserCompanyId = userCompany.Id,
                PermissionTypeId = permissionType.Id,
                IsGranted = true,
                ModuleId = defaultModule.Id
            };

            await _userCompanyPermissionRepository.AddAsync(userCompanyPermission);

            // Guardar todos los cambios
            await _unitOfWork.CompleteAsync();

            return company.Id; // Devuelve el Id de la nueva compañía
        }

        public async Task<Company> Handle(UpdateCompanyCommand command)
        {
            // Buscar la compañía
            var existingCompany = await _companyRepository.FindByIdAsync(command.Id);
            if (existingCompany == null)
                throw new Exception("Company not found");

            // Verificar si el usuario es el creador de la compañía
            if (command.CreatedById != existingCompany.CreatedById)
            {
                // No es el creador, entonces verificar si tiene permisos de ADMIN
                var userCompany = await _userCompanyRepository.FindByUserAndCompanyAsync(command.CreatedById, existingCompany.Id);
                if (userCompany == null)
                    throw new Exception("User not associated with this company.");

                // Comprobar si tiene permisos de ADMIN
                var userCompanyPermission = await _userCompanyPermissionRepository.FindByUserCompanyAndPermissionTypeAsync(
                    userCompany.Id, 
                    AllowedActionEnum.ADMIN
                );

                if (userCompanyPermission == null || !userCompanyPermission.IsGranted)
                    throw new Exception("User does not have admin rights for this company.");
            }

            // El usuario es el creador o tiene permisos de ADMIN, por lo tanto se permite la actualización
            existingCompany.CompanyName = command.CompanyName;
            existingCompany.LegalName = command.LegalName;
            existingCompany.Ruc = command.Ruc;
            existingCompany.Address = command.Address;
            existingCompany.Email = command.Email;
            existingCompany.Password = command.Password;

            // Actualizar los cambios
            await _companyRepository.UpdateAsync(existingCompany);
            await _unitOfWork.CompleteAsync();

            return existingCompany;
        }

        public async Task<Bank> CreateBank(CreateBankCommand command)
        {
            var company = await _companyRepository.FindByIdAsync(command.CompanyId);
            if (company == null)
                throw new Exception("Company not found");

            var bank = new Bank
            {
                Id = Guid.NewGuid(),
                Name = command.Name,
                CompanyId = command.CompanyId,
                AccountNumber = command.AccountNumber,
                IBAN = command.IBAN,
                SWIFT = command.SWIFT,
                AccountHolderName = command.AccountHolderName,
                AccountType = command.AccountType,
                BankAddress = command.BankAddress,
                PaymentReference = command.PaymentReference,
                TCEApreferredRate= command.TCEApreferredRate
            };

            await _bankRepository.AddAsync(bank);
            await _unitOfWork.CompleteAsync();

            return bank;
        }
    }
}
