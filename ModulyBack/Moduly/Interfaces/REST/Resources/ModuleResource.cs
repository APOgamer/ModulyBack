namespace ModulyBack.Moduly.Interfaces.REST.Resources;

public class ModuleResource
{
    public Guid Id { get; set; }
    public string ModuleName { get; set; }
    public string ModuleType { get; set; }
    public Guid CompanyId { get; set; }
    public DateTime CreationDate { get; set; }
}