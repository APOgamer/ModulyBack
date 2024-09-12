using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace ModulyBack.IAM.Domain.Model.Aggregates
{
    public class User
    {
        // Constructor sin parámetros para EF Core
        public User()
        {
        }

        // Constructor con parámetros
        public User(string username, string fullName, int age, string dni, string phoneNumber, string email, string passwordHash)
        {
            Username = username;
            FullName = fullName;
            Age = age;
            Dni = dni;
            PhoneNumber = phoneNumber;
            Email = email;
            PasswordHash = passwordHash;
        }

        [Key] // Aquí se usa el atributo [Key] para marcar la propiedad como clave primaria
        public Guid Id { get; set; } // EF Core usa esta propiedad para la clave primaria

        [Required]
        public string Username { get; set; } = string.Empty;

        [Required]
        public string FullName { get; set; } = string.Empty;

        public int Age { get; set; }

        [Required]
        public string Dni { get; set; } = string.Empty;

        [Required]
        public string PhoneNumber { get; set; } = string.Empty;

        [JsonIgnore]
        [Required]
        public string PasswordHash { get; set; } = string.Empty;

        [Required]
        [EmailAddress] // Opcional: Valida el formato del email
        public string Email { get; set; } = string.Empty;

        public DateTime CreationDate { get; set; } = DateTime.UtcNow;

        // Métodos de actualización
        public User UpdateUsername(string username)
        {
            if (username.Length > 20)
                throw new ArgumentException("Username must be 20 characters or less.");
            Username = username;
            return this;
        }

        public User UpdateFullName(string fullName)
        {
            FullName = fullName;
            return this;
        }

        public User UpdateAge(int age)
        {
            Age = age;
            return this;
        }

        public User UpdateDni(string dni)
        {
            Dni = dni;
            return this;
        }

        public User UpdatePhoneNumber(string phoneNumber)
        {
            PhoneNumber = phoneNumber;
            return this;
        }

        public User UpdatePasswordHash(string passwordHash)
        {
            PasswordHash = passwordHash;
            return this;
        }

        public User UpdateEmail(string email)
        {
            if (email.Length > 255)
                throw new ArgumentException("Email must be 255 characters or less.");
            Email = email;
            return this;
        }
    }
}
