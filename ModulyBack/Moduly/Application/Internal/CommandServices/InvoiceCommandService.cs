using ModulyBack.Moduly.Domain.Model.Commands;
using ModulyBack.Moduly.Domain.Model.Entities;
using ModulyBack.Moduly.Domain.Repositories;
using ModulyBack.Moduly.Domain.Services;
using ModulyBack.Shared.Domain.Repositories;

namespace ModulyBack.Moduly.Application.Internal.CommandServices;

public class InvoiceCommandService : IInvoiceCommandService
{
    private readonly IInvoiceRepository _invoiceRepository;
    private readonly IUnitOfWork _unitOfWork;

    public InvoiceCommandService(IInvoiceRepository invoiceRepository, IUnitOfWork unitOfWork)
    {
        _invoiceRepository = invoiceRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Invoice> Handle(CreateInvoiceCommand command)
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
            Status = command.Status
        };

        await _invoiceRepository.AddAsync(invoice);
        await _unitOfWork.CompleteAsync();
        return invoice;
    }

    public async Task Handle(UpdateInvoiceCommand command)
    {
        var existingInvoice = await _invoiceRepository.FindByIdAsync(command.Id);
        if (existingInvoice == null)
            throw new Exception("Invoice not found");

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
    public async Task Handle(DeleteInvoiceCommand command)
    {
        var invoice = await _invoiceRepository.FindByIdAsync(command.InvoiceId);
        if (invoice == null)
            throw new Exception("Invoice not found");

        await _invoiceRepository.DeleteAsync(invoice.Id);
        await _unitOfWork.CompleteAsync();
    }

    public async Task<Invoice> Handle(UpdateInvoiceStatusCommand command)
    {
        var invoice = await _invoiceRepository.FindByIdAsync(command.InvoiceId);
        if (invoice == null)
            throw new Exception("Invoice not found");

        invoice.Status = command.Status;
        await _invoiceRepository.UpdateAsync(invoice);
        await _unitOfWork.CompleteAsync();
        return invoice;
    }
}