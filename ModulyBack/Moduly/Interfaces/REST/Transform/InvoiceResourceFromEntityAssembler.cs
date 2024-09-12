using ModulyBack.Moduly.Domain.Model.Entities;
using ModulyBack.Moduly.Interfaces.REST.Resources;

namespace ModulyBack.Moduly.Interfaces.REST.Transform
{
    public static class InvoiceResourceFromEntityAssembler
    {
        public static InvoiceResource ToResourceFromEntity(Invoice invoice)
        {
            return new InvoiceResource
            {
                Id = invoice.Id,
                Code = invoice.Code,
                ModuleId = invoice.ModuleId,
                IssuerId = invoice.IssuerId,
                IssueDate = invoice.IssueDate,
                DueDate = invoice.DueDate,
                Description = invoice.Description,
                Quantity = invoice.Quantity,
                UnitPrice = invoice.UnitPrice, // Cambio: puede ser nulo
                TotalPayment = invoice.TotalPayment,
                Status = invoice.Status,
                Currency = invoice.Currency, // Nuevo campo
                ExchangeRate = invoice.ExchangeRate, // Nuevo campo
                DiscountDate = invoice.DiscountDate, // Nuevo campo
                TCEA = invoice.TCEA // Nuevo campo
            };
        }
    }
}