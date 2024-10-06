using ModulyBack.Moduly.Domain.Model.Entities;
using ModulyBack.Moduly.Interfaces.REST.Resources;

namespace ModulyBack.Moduly.Interfaces.REST.Transform;

public static class CompanyResourceFromEntityAssembler
{
    public static CompanyResource ToResourceFromEntity(Company company)
    {
        return new CompanyResource(
            company.Id,
            company.CompanyName,
            company.LegalName,
            company.Ruc,
            company.Address,
            company.Email,
            company.Password,
            company.Banks.Select(b => new BankResource(b.Id, b.Name, b.TCEA))
        );
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