namespace ModulyBack.Moduly.Domain.Model.Commands;

public record UpdateCompanyCommand(
    Guid Id,
    string CompanyName,
    string LegalName,
    string Ruc,
    string Address,
    string Email,
    string Password,
    Guid? CreatedById
);