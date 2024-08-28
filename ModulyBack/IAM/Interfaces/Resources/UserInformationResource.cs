namespace ModulyBack.IAM.Interfaces.Resources;

public record UserInformationResource(Guid Id, string Username,string password,string role, string name,string lastname,string correo);