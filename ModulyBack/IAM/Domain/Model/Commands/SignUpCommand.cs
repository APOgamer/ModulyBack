public class SignUpCommand
{
    public string Username { get; }
    public string Password { get; }
    public string FullName { get; }
    public int Age { get; }
    public string Dni { get; }
    public string PhoneNumber { get; }
    public string Email { get; }

    public SignUpCommand(string username, string password, string fullName, int age, string dni, string phoneNumber, string email)
    {
        Username = username;
        Password = password;
        FullName = fullName;
        Age = age;
        Dni = dni;
        PhoneNumber = phoneNumber;
        Email = email;
    }
}