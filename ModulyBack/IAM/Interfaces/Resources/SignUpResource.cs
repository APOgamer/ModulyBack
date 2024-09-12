
namespace ModulyBack.IAM.Interfaces.Resources;

public record SignUpResource(
    string Username,
    string Password,
    string FullName,        
    int Age,                
    string Dni,             
    string PhoneNumber,     
    string Email);