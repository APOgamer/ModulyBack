using Humanizer;
using ModulyBack.Moduly.Domain.Model.Commands;
using ModulyBack.Moduly.Domain.Model.Entities;

namespace ModulyBack.Moduly.Domain.Services;

public interface IInvoiceCommandService
{
    Task<Invoice> Handle(CreateInvoiceCommand command);
    Task Handle(UpdateInvoiceCommand command);
    Task Handle(DeleteInvoiceCommand command); 
    Task<Invoice> Handle(UpdateInvoiceStatusCommand command); 
}