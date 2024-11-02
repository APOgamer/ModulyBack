using ModulyBack.Moduly.Domain.Model.Commands;
using ModulyBack.Moduly.Interfaces.REST.Resources;

namespace ModulyBack.Moduly.Interfaces.REST.Transform;

public static class CreateBankCommandFromResourceAssembler
{
    public static CreateBankCommand ToCommandFromResource(Guid companyId, CreateBankResource resource)
    {
        return new CreateBankCommand(
            CompanyId: companyId,
            Name: resource.Name,
            AccountNumber: resource.AccountNumber,
            IBAN: resource.IBAN,
            SWIFT: resource.SWIFT,
            AccountHolderName: resource.AccountHolderName,
            AccountType: resource.AccountType,
            BankAddress: resource.BankAddress,
            PaymentReference: resource.PaymentReference,
            TCEApreferredRate: resource.TCEApreferredRate
        );
    }
}