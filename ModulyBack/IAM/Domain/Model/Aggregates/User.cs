using Newtonsoft.Json;

namespace ModulyBack.IAM.Domain.Model.Aggregates
{
    public class User(
        string username,
        string passwordHash,
        string firstName,
        string lastName,
        string email,
        string role = "alumno")
    {
        public User() : this(String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty)
        {

        }

        public User(Guid id) : this()
        {
            Id = id;
        }

        public Guid Id { get; set; }



        public string Username { get; set; } = username;

        [JsonIgnore] public string PasswordHash { get; set; } = passwordHash;

        public string FirstName { get; set; } = firstName;

        public string LastName { get; set; } = lastName;

        public string Email { get; set; } = email;

        public string Role { get; set; } = role;

        // Propiedades de navegación para la relación con Rooms y Questions







        public User UpdateUsername(string username)
        {
            Username = username.Length <= 20
                ? username
                : throw new ArgumentException("Username must be 20 characters or less.");
            return this;
        }

        public User UpdatePasswordHash(string passwordHash)
        {
            PasswordHash = passwordHash;
            return this;
        }

        public User UpdateFirstName(string firstName)
        {
            FirstName = firstName;
            return this;
        }

        public User UpdateLastName(string lastName)
        {
            LastName = lastName.Length <= 50
                ? lastName
                : throw new ArgumentException("Last name must be 50 characters or less.");
            return this;
        }

        public User UpdateEmail(string email)
        {
            Email = email.Length <= 100 ? email : throw new ArgumentException("Email must be 100 characters or less.");
            return this;
        }

        public User UpdateRole(string role)
        {
            Role = role.Length <= 20 ? role : throw new ArgumentException("Role must be 20 characters or less.");
            return this;
        }
    }
}