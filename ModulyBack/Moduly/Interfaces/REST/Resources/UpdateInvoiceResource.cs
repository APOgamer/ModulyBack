namespace ModulyBack.Moduly.Interfaces.REST.Resources;

public class UpdateInvoiceResource
{
    public string Code { get; set; }
    public DateTime? DueDate { get; set; }
    public string Description { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal TotalPayment => Quantity * UnitPrice;
    public string Status { get; set; }
    public Guid UserCompanyId { get; set; }
}