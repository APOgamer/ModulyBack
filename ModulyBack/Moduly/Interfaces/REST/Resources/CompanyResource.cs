using System;
using System.Collections.Generic;

namespace ModulyBack.Moduly.Interfaces.REST.Resources;

public class CompanyResource
{
    public Guid Id { get; set; }
    public string CompanyName { get; set; }
    public string LegalName { get; set; }
    public string Ruc { get; set; }
    public string Address { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public Guid CreatedById { get; set; }
    public List<BankResource> Banks { get; set; } = new List<BankResource>();
}

