namespace ModulyBack.Moduly.Interfaces.REST.Resources;

public class PermissionTypeResource
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public Guid CompanyId { get; set; }
    public string CompanyName { get; set; } = string.Empty;
    public ICollection<PermissionTypeActionResource> PermissionTypeActions { get; set; } = new List<PermissionTypeActionResource>();
}

