public class CreateBankResource
{
    public string Name { get; set; }
    public string? AccountNumber { get; set; }
    public string? IBAN { get; set; }
    public string? SWIFT { get; set; }
    public string? AccountHolderName { get; set; }
    public string? AccountType { get; set; }
    public string? BankAddress { get; set; }
    public string? PaymentReference { get; set; }
    public float? TCEApreferredRate {get;set;}

}