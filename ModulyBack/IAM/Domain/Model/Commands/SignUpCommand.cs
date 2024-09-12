namespace ModulyBack.IAM.Domain.Model.Commands
{
    public record SignUpCommand(
        string Username, 
        string Password, 
        string FullName, // Reemplaza Name y LastName con FullName
        int Age,         // Añade Age
        string Dni,      // Añade Dni
        string PhoneNumber, // Añade PhoneNumber
        string Email, 
        string Role
    )
    {
        public void Validate()
        {
            if (string.IsNullOrEmpty(Username))
                throw new ArgumentException("Username cannot be empty.");
            if (string.IsNullOrEmpty(Password))
                throw new ArgumentException("Password cannot be empty.");
            if (string.IsNullOrEmpty(FullName))
                throw new ArgumentException("Full name cannot be empty.");
            if (Age <= 0)
                throw new ArgumentException("Age must be a positive number.");
            if (string.IsNullOrEmpty(Dni))
                throw new ArgumentException("DNI cannot be empty.");
            if (string.IsNullOrEmpty(PhoneNumber))
                throw new ArgumentException("Phone number cannot be empty.");
            if (string.IsNullOrEmpty(Email))
                throw new ArgumentException("Email cannot be empty.");
            // Validaciones adicionales si es necesario
            Console.WriteLine("Validado");
        }
    }
}