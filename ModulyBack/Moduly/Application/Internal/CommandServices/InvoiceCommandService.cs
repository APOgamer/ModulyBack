using ModulyBack.IAM.Domain.Repositories;
using ModulyBack.Moduly.Domain.Model.Commands;
using ModulyBack.Moduly.Domain.Model.Entities;
using ModulyBack.Moduly.Domain.Model.ValueObjects;
using ModulyBack.Moduly.Domain.Repositories;
using ModulyBack.Moduly.Domain.Services;
using ModulyBack.Shared.Domain.Repositories;

namespace ModulyBack.Moduly.Application.Internal.CommandServices
{
    public class InvoiceCommandService : IInvoiceCommandService
    {
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserCompanyPermissionRepository _userCompanyPermissionRepository;
        private readonly IUserCompanyRepository _userCompanyRepository;
        private readonly ICompanyRepository _companyRepository;

        public InvoiceCommandService(
            IInvoiceRepository invoiceRepository, 
            IUnitOfWork unitOfWork,
            IUserCompanyPermissionRepository userCompanyPermissionRepository,
            IUserCompanyRepository userCompanyRepository,
            ICompanyRepository companyRepository)
        {
            _invoiceRepository = invoiceRepository;
            _unitOfWork = unitOfWork;
            _userCompanyPermissionRepository = userCompanyPermissionRepository;
            _userCompanyRepository = userCompanyRepository;
            _companyRepository = companyRepository;
        }

        public async Task<Invoice> Handle(CreateInvoiceCommand command)
        {
            // Obtener el UserCompanyId
            var userCompanyId = await _userCompanyRepository.FindUserCompanyIdByUserId(command.UserId);
            if (!userCompanyId.HasValue)
            {
                throw new UnauthorizedAccessException("User is not associated with any company.");
            }

            // Verificar si el usuario es el creador de la compañía
            var company = await _companyRepository.FindByModuleIdAsync(command.ModuleId);
            if (company != null && company.CreatedById == command.UserId)
            {
                // El usuario es el creador, tiene permiso automático
                return await CreateInvoice(command);
            }

            // Si no es el creador, verificar permisos
            var userPermission = await _userCompanyPermissionRepository
                .FindByUserCompanyAndPermissionTypeInModuleAsync(userCompanyId.Value, command.ModuleId, AllowedActionEnum.CREATE_INVOICE);

            if (userPermission == null)
                throw new UnauthorizedAccessException($"User does not have permission to create invoices in ModuleId: {command.ModuleId}");

            return await CreateInvoice(command);
        }

        private async Task<Invoice> CreateInvoice(CreateInvoiceCommand command)
        {
            var invoice = new Invoice
            {
                Code = command.Code,
                ModuleId = command.ModuleId,
                IssuerId = command.IssuerId,
                IssueDate = command.IssueDate,
                DueDate = command.DueDate,
                Description = command.Description,
                Quantity = command.Quantity,
                UnitPrice = command.UnitPrice,
                TotalPayment = command.TotalPayment,
                Status = command.Status,
                ExchangeRate = command.exchangeRate,
                DiscountDate = command.discountDate,
                TCEA = command.tcea
            };

            await _invoiceRepository.AddAsync(invoice);
            await _unitOfWork.CompleteAsync();
            return invoice;
        }

        // Verificar permisos antes de actualizar una factura en el módulo especificado
        public async Task Handle(UpdateInvoiceCommand command)
        {
            var existingInvoice = await _invoiceRepository.FindByIdAsync(command.Id);
            if (existingInvoice == null)
                throw new Exception("Invoice not found");

            var userPermission = await _userCompanyPermissionRepository
                .FindByUserCompanyAndPermissionTypeInModuleAsync(command.UserCompanyId, existingInvoice.ModuleId, AllowedActionEnum.EDIT_INVOICE);

            if (userPermission == null)
                throw new Exception("User does not have permission to edit invoices in this module.");

            existingInvoice.Code = command.Code;
            existingInvoice.ModuleId = command.ModuleId;
            existingInvoice.IssuerId = command.IssuerId;
            existingInvoice.IssueDate = command.IssueDate;
            existingInvoice.DueDate = command.DueDate;
            existingInvoice.Description = command.Description;
            existingInvoice.Quantity = command.Quantity;
            existingInvoice.UnitPrice = command.UnitPrice;
            existingInvoice.TotalPayment = command.TotalPayment;
            existingInvoice.Status = command.Status;
            existingInvoice.ExchangeRate = command.exchangeRate;
            existingInvoice.DiscountDate = command.discountDate;
            existingInvoice.TCEA = command.tcea;

            await _invoiceRepository.UpdateAsync(existingInvoice);
            await _unitOfWork.CompleteAsync();
        }

        // Verificar permisos antes de eliminar una factura en el módulo especificado
        public async Task Handle(DeleteInvoiceCommand command)
        {
            var invoice = await _invoiceRepository.FindByIdAsync(command.InvoiceId);
            if (invoice == null)
                throw new Exception("Invoice not found");

            // Get the company associated with the invoice's module
            var company = await _companyRepository.FindByModuleIdAsync(invoice.ModuleId);
            if (company == null)
                throw new Exception("Company not found for this invoice's module");

            // Check if the user is the company creator
            var userCompany = await _userCompanyRepository.FindByIdAsync(command.UserCompanyId);
            if (userCompany == null)
                throw new Exception("UserCompany not found");

            if (company.CreatedById == userCompany.UserId)
            {
                // User is the creator, skip permission check
                await _invoiceRepository.DeleteAsync(invoice.Id);
                await _unitOfWork.CompleteAsync();
                return;
            }

            // If not the creator, proceed with the regular permission check
            var userPermission = await _userCompanyPermissionRepository
                .FindByUserCompanyAndPermissionTypeInModuleAsync(command.UserCompanyId, invoice.ModuleId, AllowedActionEnum.DELETE_INVOICE);

            if (userPermission == null)
                throw new Exception("User does not have permission to delete invoices in this module.");

            await _invoiceRepository.DeleteAsync(invoice.Id);
            await _unitOfWork.CompleteAsync();
        }

        // Verificar permisos antes de actualizar el estado de una factura en el módulo especificado
        public async Task<Invoice> Handle(UpdateInvoiceStatusCommand command)
        {
            var invoice = await _invoiceRepository.FindByIdAsync(command.InvoiceId);
            if (invoice == null)
                throw new Exception("Invoice not found");

            // Get the company associated with the invoice's module
            var company = await _companyRepository.FindByModuleIdAsync(invoice.ModuleId);
            if (company == null)
                throw new Exception("Company not found for this invoice's module");

            // Check if the user is the company creator
            var userCompany = await _userCompanyRepository.FindByIdAsync(command.UserCompanyId);
            if (userCompany == null)
                throw new Exception("UserCompany not found");

            if (company.CreatedById == userCompany.UserId)
            {
                // User is the creator, skip permission check
                invoice.Status = command.Status;
                await _invoiceRepository.UpdateAsync(invoice);
                await _unitOfWork.CompleteAsync();
                return invoice;
            }

            // If not the creator, proceed with the regular permission check
            var userPermission = await _userCompanyPermissionRepository
                .FindByUserCompanyAndPermissionTypeInModuleAsync(command.UserCompanyId, invoice.ModuleId, AllowedActionEnum.EDIT_INVOICE);

            if (userPermission == null)
                throw new Exception("User does not have permission to edit invoice status in this module.");

            invoice.Status = command.Status;
            await _invoiceRepository.UpdateAsync(invoice);
            await _unitOfWork.CompleteAsync();
            return invoice;
        }
    }
}
