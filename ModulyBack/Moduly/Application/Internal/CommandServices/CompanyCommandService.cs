using ModulyBack.Moduly.Domain.Model.Commands;
using ModulyBack.Moduly.Domain.Model.Entities;
using ModulyBack.Moduly.Domain.Repositories;
using ModulyBack.Shared.Domain.Repositories;
using System;
using System.Threading.Tasks;
using ModulyBack.Moduly.Domain.Services;

namespace ModulyBack.Moduly.Application.Internal.CommandServices
{
    public class CompanyCommandService : ICompanyCommandService
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CompanyCommandService(ICompanyRepository companyRepository, IUnitOfWork unitOfWork)
        {
            _companyRepository = companyRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(CreateCompanyCommand command)
        {
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
            await _unitOfWork.CompleteAsync();
        }

        public async Task Handle(UpdateCompanyCommand command)
        {
            var existingCompany = await _companyRepository.FindByIdAsync(command.Id);
            if (existingCompany == null)
                throw new Exception("Company not found");

            existingCompany.CompanyName = command.CompanyName;
            existingCompany.LegalName = command.LegalName;
            existingCompany.Ruc = command.Ruc;
            existingCompany.Address = command.Address;
            existingCompany.Email = command.Email;
            existingCompany.Password = command.Password;
            // Actualiza otros campos según sea necesario

            await _companyRepository.UpdateAsync(existingCompany);
            await _unitOfWork.CompleteAsync();
        }
    }
}
