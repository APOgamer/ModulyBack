using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace ModulyBack.Moduly.Domain.Model.Entities
{
    public class Company
    {
        [Key] // Aquí se usa el atributo [Key] para marcar la propiedad como clave primaria
        public Guid Id { get; set; }

        [Required] // Asegura que CompanyName no sea nulo
        public string CompanyName { get; set; } = string.Empty;

        [Required] // Asegura que LegalName no sea nulo
        public string LegalName { get; set; } = string.Empty;

        [Required] // Asegura que Ruc no sea nulo
        public string Ruc { get; set; } = string.Empty;

        [Required] // Asegura que Address no sea nulo
        public string Address { get; set; } = string.Empty;

        [Required]
        [EmailAddress] // Opcional: Valida el formato del email
        public string Email { get; set; } = string.Empty;

        [Required] // Asegura que Password no sea nulo
        public string Password { get; set; } = string.Empty;

        public DateTime CreationDate { get; set; } = DateTime.UtcNow;

        public Guid? CreatedById { get; set; }

        public ICollection<UserCompany> UserCompanies { get; set; } = new List<UserCompany>();
        public ICollection<Module> Modules { get; set; } = new List<Module>();
    }
}