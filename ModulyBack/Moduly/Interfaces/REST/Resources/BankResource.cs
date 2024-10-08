namespace ModulyBack.Moduly.Interfaces.REST.Resources;
public record BankResource(
    Guid Id,
    string Name,
    string? AccountNumber,
    string? IBAN,
    string? SWIFT,
    string? AccountHolderName,
    string? AccountType,
    string? BankAddress,
    string? PaymentReference
);