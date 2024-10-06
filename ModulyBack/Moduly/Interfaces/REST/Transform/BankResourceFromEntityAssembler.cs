using ModulyBack.Moduly.Domain.Model.Entities;
using ModulyBack.Moduly.Interfaces.REST.Resources;

namespace ModulyBack.Moduly.Interfaces.REST.Transform;

public static class BankResourceFromEntityAssembler
{
    public static BankResource ToResourceFromEntity(Bank bank)
    {
        return new BankResource(
            bank.Id,
            bank.Name,
            bank.AccountNumber,
            bank.IBAN,
            bank.SWIFT,
            bank.AccountHolderName,
            bank.AccountType,
            bank.BankAddress,
            bank.PaymentReference
        );
    }
}
