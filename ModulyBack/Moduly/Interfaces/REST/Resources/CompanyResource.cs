namespace ModulyBack.Moduly.Interfaces.REST.Resources;

public record CompanyResource(
    Guid Id,
    string CompanyName,
    string LegalName,
    string Ruc,
    string Address,
    string Email,
    string Password,
    IEnumerable<BankResource> Banks
);

public record BankResource(
    Guid Id,
    string Name,
    decimal TCEA
);