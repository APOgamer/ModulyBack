namespace ModulyBack.Moduly.Interfaces.REST.Resources;

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
    public decimal UnitPrice { get; set; }
    public decimal TotalPayment { get; set; }
    public string Status { get; set; }
}