
namespace ModulyBack.IAM.Interfaces.Resources;

public record AuthenticatedUserResource(Guid Id, string Usernam, string Token);