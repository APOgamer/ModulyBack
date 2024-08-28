namespace ModulyBack.IAM.Domain.Model.Commands
{
    public record SignUpCommand(string Username, string Password, string Role, string Name, string LastName, string Correo)
    {
        public void Validate()
        {
           Console.WriteLine("Validado");
        }
    }
}