using System.ComponentModel.DataAnnotations;

public class CreateInvoiceResource
{
    [Required(ErrorMessage = "The Code field is required.")]
    public string Code { get; set; }

    [Required(ErrorMessage = "The ModuleId field is required.")]
    public Guid ModuleId { get; set; }

    [Required(ErrorMessage = "The IssuerId field is required.")]
    public Guid? IssuerId { get; set; }

    [Required(ErrorMessage = "The IssueDate field is required.")]
    public DateTime IssueDate { get; set; }

    public DateTime? DueDate { get; set; }

    public string Description { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Quantity must be greater than 0.")]
    public int Quantity { get; set; }

    [Range(0.01, double.MaxValue, ErrorMessage = "Unit Price must be greater than 0.")]
    public decimal UnitPrice { get; set; }

    [Required(ErrorMessage = "The Status field is required.")]
    public string Status { get; set; }

    [Required(ErrorMessage = "The UserId field is required.")]
    public Guid UserId { get; set; }
}