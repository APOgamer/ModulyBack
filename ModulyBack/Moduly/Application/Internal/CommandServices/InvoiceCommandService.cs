using ModulyBack.IAM.Domain.Repositories;
using ModulyBack.Moduly.Domain.Model.Commands;
using ModulyBack.Moduly.Domain.Model.Entities;
using ModulyBack.Moduly.Domain.Model.ValueObjects;
using ModulyBack.Moduly.Domain.Repositories;
using ModulyBack.Moduly.Domain.Services;
using ModulyBack.Shared.Domain.Repositories;

namespace ModulyBack.Moduly.Application.Internal.CommandServices;
    public class InvoiceCommandService : IInvoiceCommandService
    {
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserCompanyPermissionRepository _userCompanyPermissionRepository;
        private readonly IUserCompanyRepository _UserCompanyRepository;
        public InvoiceCommandService(
            IInvoiceRepository invoiceRepository, 
            IUnitOfWork unitOfWork,
            IUserCompanyPermissionRepository userCompanyPermissionRepository,
            IUserCompanyRepository userCompanyRepository)
        {
            _invoiceRepository = invoiceRepository;
            _unitOfWork = unitOfWork;
            _userCompanyPermissionRepository = userCompanyPermissionRepository;
            _UserCompanyRepository = userCompanyRepository;
        }

        // Verificar permisos antes de crear una factura en el módulo especificado
        public async Task<Invoice> Handle(CreateInvoiceCommand command)
        {
            var UserCommandId = await _UserCompanyRepository.FindUserCompanyIdByUserId(command.UserId);
            if (!UserCommandId.HasValue)
                throw new Exception($"UserCompanyId not found for UserId: {command.UserId}");
    
            var userPermission = await _userCompanyPermissionRepository
                .FindByUserCompanyAndPermissionTypeInModuleAsync(UserCommandId.Value, command.ModuleId, AllowedActionEnum.CREATE_INVOICE);

            if (userPermission == null)
                throw new Exception($"User does not have permission to create invoices in ModuleId: {command.ModuleId}");

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
                Status = command.Status
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

            await _invoiceRepository.UpdateAsync(existingInvoice);
            await _unitOfWork.CompleteAsync();
        }

        // Verificar permisos antes de eliminar una factura en el módulo especificado
        public async Task Handle(DeleteInvoiceCommand command)
        {
            var invoice = await _invoiceRepository.FindByIdAsync(command.InvoiceId);
            if (invoice == null)
                throw new Exception("Invoice not found");
            
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