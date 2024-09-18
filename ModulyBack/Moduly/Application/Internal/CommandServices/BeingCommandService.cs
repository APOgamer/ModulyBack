using ModulyBack.Moduly.Domain.Model.Aggregate;
using ModulyBack.Moduly.Domain.Model.Commands;
using ModulyBack.Moduly.Domain.Model.Entities;
using ModulyBack.Moduly.Domain.Model.ValueObjects;
using ModulyBack.Moduly.Domain.Repositories;
using ModulyBack.Moduly.Domain.Services;
using ModulyBack.Shared.Domain.Repositories;

namespace ModulyBack.Moduly.Application.Internal.CommandServices;

    public class BeingCommandService : IBeingCommandService
    {
        private readonly IBeingRepository _beingRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserCompanyPermissionRepository _userCompanyPermissionRepository;
        private readonly IUserCompanyRepository _userCompanyRepository;
        private readonly ICompanyRepository _companyRepository;

        public BeingCommandService(
            IBeingRepository beingRepository,
            IUnitOfWork unitOfWork,
            IUserCompanyPermissionRepository userCompanyPermissionRepository,
            IUserCompanyRepository userCompanyRepository,
            ICompanyRepository companyRepository)
        {
            _beingRepository = beingRepository;
            _unitOfWork = unitOfWork;
            _userCompanyPermissionRepository = userCompanyPermissionRepository;
            _userCompanyRepository = userCompanyRepository;
            _companyRepository = companyRepository;
        }

        public async Task<Being> Handle(CreateBeingCommand command)
        {
            var userCompanyId = await _userCompanyRepository.FindUserCompanyIdByUserId(command.UserId);
            if (!userCompanyId.HasValue)
            {
                throw new UnauthorizedAccessException("User is not associated with any company.");
            }

            var company = await _companyRepository.FindByModuleIdAsync(command.ModuleId);
            if (company != null && company.CreatedById == command.UserId)
            {
                return await CreateBeing(command);
            }

            var userPermission = await _userCompanyPermissionRepository
                .FindByUserCompanyAndPermissionTypeInModuleAsync(userCompanyId.Value, command.ModuleId,
                    AllowedActionEnum.CREATE_BEING);

            if (userPermission == null)
                throw new UnauthorizedAccessException(
                    $"User does not have permission to create beings in ModuleId: {command.ModuleId}");

            return await CreateBeing(command);
        }

        private async Task<Being> CreateBeing(CreateBeingCommand command)
        {
            var being = new Being
            {
                IdentifierName = command.IdentifierName,
                Description = command.Description,
                Price = command.Price,
                Category = command.Category,
                ManufactureDate = command.ManufactureDate,
                ExpiryDate = command.ExpiryDate,
                Origin = command.Origin,
                Tags = command.Tags,
                IsActive = command.IsActive,
                Discount = command.Discount,
                Currency = command.Currency,
                ImageUrl = command.ImageUrl,
                BeingModules = new List<BeingModule> { new BeingModule { ModuleId = command.ModuleId } }
            };

            await _beingRepository.AddAsync(being);
            await _unitOfWork.CompleteAsync();
            return being;
        }

        public async Task<Being> Handle(UpdateBeingCommand command)
        {
            var existingBeing = await _beingRepository.FindByIdAsync(command.Id);
            if (existingBeing == null)
                throw new Exception("Being not found");

            var userCompanyId = await _userCompanyRepository.FindUserCompanyIdByUserId(command.UserId);
            if (!userCompanyId.HasValue)
            {
                throw new UnauthorizedAccessException("User is not associated with any company.");
            }

            var userPermission = await _userCompanyPermissionRepository
                .FindByUserCompanyAndPermissionTypeInModuleAsync(userCompanyId.Value, command.ModuleId,
                    AllowedActionEnum.EDIT_BEING);

            if (userPermission == null)
                throw new UnauthorizedAccessException("User does not have permission to edit beings in this module.");

            existingBeing.IdentifierName = command.IdentifierName;
            existingBeing.Description = command.Description;
            existingBeing.Price = command.Price;
            existingBeing.Category = command.Category;
            existingBeing.ManufactureDate = command.ManufactureDate;
            existingBeing.ExpiryDate = command.ExpiryDate;
            existingBeing.Origin = command.Origin;
            existingBeing.Tags = command.Tags;
            existingBeing.IsActive = command.IsActive;
            existingBeing.Discount = command.Discount;
            existingBeing.Currency = command.Currency;
            existingBeing.ImageUrl = command.ImageUrl;
            existingBeing.UpdatedAt = DateTime.UtcNow;

            await _beingRepository.UpdateAsync(existingBeing);
            await _unitOfWork.CompleteAsync();
            return existingBeing;
        }

        public async Task Handle(DeleteBeingCommand command)
        {
            var being = await _beingRepository.FindByIdAsync(command.Id);
            if (being == null)
                throw new Exception("Being not found");

            var userPermission = await _userCompanyPermissionRepository
                .FindByUserCompanyAndPermissionTypeInModuleAsync(command.UserCompanyId,
                    being.BeingModules.First().ModuleId, AllowedActionEnum.DELETE_BEING);

            if (userPermission == null)
                throw new UnauthorizedAccessException("User does not have permission to delete beings in this module.");

            await _beingRepository.DeleteAsync(being.Id);
            await _unitOfWork.CompleteAsync();
        }


    }
