using System;
using System.Collections.Generic;
using ModulyBack.Moduly.Domain.Model.Aggregates;

namespace ModulyBack.Moduly.Domain.Model.Entities
{
    public class Company
    {
        public Guid Id { get; set; }
        public string CompanyName { get; set; }
        public string LegalName { get; set; }
        public string Ruc { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime CreationDate { get; set; }
        public Guid? CreatedById { get; set; }
        public ICollection<UserCompany> UserCompanies { get; set; } = new List<UserCompany>();
        public ICollection<Module> Modules { get; set; } = new List<Module>();
    }
}