namespace ModulyBack.IAM.Interfaces.Resources;

public class CreateUserResource
{
    public string Name { get; set; }
    public string LastName { get; set; }
    public string Correo { get; set; }
    public string Password { get; set; }
    public long? CodigoProfesor { get; set; }
}
