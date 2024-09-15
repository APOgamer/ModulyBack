namespace ModulyBack.Moduly.Interfaces.REST.Resources;

public class PermissionTypeActionResource
{
    public Guid Id { get; set; }
    public string AllowedAction { get; set; } = string.Empty;
}