namespace ModulyBack.Moduly.Interfaces.REST.Resources
{
    public class InvoiceResource
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public Guid ModuleId { get; set; }
        public Guid? IssuerId { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime? DueDate { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public decimal? UnitPrice { get; set; } // Cambio: puede ser nulo
        public decimal TotalPayment { get; set; }
        public string Status { get; set; }
        public string Currency { get; set; } // Nuevo campo
        public decimal? ExchangeRate { get; set; } // Nuevo campo
        public DateTime? DiscountDate { get; set; } // Nuevo campo
        public decimal? TCEA { get; set; } // Nuevo campo
    }
}