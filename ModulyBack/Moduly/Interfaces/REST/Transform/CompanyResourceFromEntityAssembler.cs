using ModulyBack.Moduly.Domain.Model.Entities;
using ModulyBack.Moduly.Interfaces.REST.Resources;

namespace ModulyBack.Moduly.Interfaces.REST.Transform;

public static class CompanyResourceFromEntityAssembler
{
    public static CompanyResource ToResourceFromEntity(Company company)
    {
        return new CompanyResource
        {
            Id = company.Id,
            CompanyName = company.CompanyName,
            LegalName = company.LegalName,
            Ruc = company.Ruc,
            Address = company.Address,
            Email = company.Email,
            Password = company.Password,
            CreatedById = company.CreatedById,
            Banks = company.Banks.Select(b => new BankResource(
                b.Id,
                b.Name,
                b.AccountNumber,
                b.IBAN,
                b.SWIFT,
                b.AccountHolderName,
                b.AccountType,
                b.BankAddress,
                b.PaymentReference
            )).ToList() // Convertimos la selección a una lista
        };
    }


    public static Company ToEntityFromResource(CompanyResource companyResource)
    {
        if (companyResource == null)
            throw new ArgumentNullException(nameof(companyResource));

        return new Company
        {
            Id = companyResource.Id,
            CompanyName = companyResource.CompanyName,
            LegalName = companyResource.LegalName,
            Ruc = companyResource.Ruc,
            Address = companyResource.Address,
            Email = companyResource.Email,
            Password = companyResource.Password
        };
    }
}