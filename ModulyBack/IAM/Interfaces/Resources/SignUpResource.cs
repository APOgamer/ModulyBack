
namespace ModulyBack.IAM.Interfaces.Resources;

public record SignUpResource(string Username,
    string Password, string Role, string Name, string LastName, string Correo);