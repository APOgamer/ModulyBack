namespace ModulyBack.Moduly.Interfaces.Resources;

public record CompanyResource(
    Guid Id,
    string CompanyName,
    string LegalName,
    string Ruc,
    string Address,
    string Email,
    string Password
);