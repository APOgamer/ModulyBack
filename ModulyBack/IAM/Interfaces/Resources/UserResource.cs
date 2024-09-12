
namespace ModulyBack.IAM.Interfaces.Resources;

public record UserResource(Guid Id, string username, string fullName, int age, string dni, string phoneNumber, string email);