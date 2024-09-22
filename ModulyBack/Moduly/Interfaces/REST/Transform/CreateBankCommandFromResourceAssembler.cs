public static class CreateBankCommandFromResourceAssembler
{
    public static CreateBankCommand ToCommandFromResource(Guid companyId, CreateBankResource resource)
    {
        return new CreateBankCommand(companyId, resource.Name, resource.TCEA);
    }
}