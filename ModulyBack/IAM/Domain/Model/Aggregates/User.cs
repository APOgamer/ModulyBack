using Newtonsoft.Json;
using System;

namespace ModulyBack.IAM.Domain.Model.Aggregates
{
    public class User
    {
        // Constructor sin parámetros para EF Core
        public User()
        {
        }

        // Constructor con parámetros
        public User(string username, string passwordHash, string firstName, string lastName, string email, string role = "user")
        {
            Username = username;
            PasswordHash = passwordHash;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Role = role;
        }

        public Guid Id { get; set; } // EF Core usa esta propiedad para la clave primaria

        public string Username { get; set; } = string.Empty;

        [JsonIgnore]
        public string PasswordHash { get; set; } = string.Empty;

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Role { get; set; } = "user";

        // Métodos de actualización
        public User UpdateUsername(string username)
        {
            if (username.Length > 20)
                throw new ArgumentException("Username must be 20 characters or less.");
            Username = username;
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
            if (lastName.Length > 50)
                throw new ArgumentException("Last name must be 50 characters or less.");
            LastName = lastName;
            return this;
        }

        public User UpdateEmail(string email)
        {
            if (email.Length > 100)
                throw new ArgumentException("Email must be 100 characters or less.");
            Email = email;
            return this;
        }

        public User UpdateRole(string role)
        {
            if (role.Length > 20)
                throw new ArgumentException("Role must be 20 characters or less.");
            Role = role;
            return this;
        }
    }
}
