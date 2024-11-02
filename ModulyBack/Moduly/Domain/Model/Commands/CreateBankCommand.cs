public record CreateBankCommand(
    Guid CompanyId,
    string Name,
    string? AccountNumber = null,
    string? IBAN = null,
    string? SWIFT = null,
    string? AccountHolderName = null,
    string? AccountType = null,
    string? BankAddress = null,
    string? PaymentReference = null,
    float? TCEApreferredRate = null
);